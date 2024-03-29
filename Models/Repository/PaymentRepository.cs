﻿using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Enums;
using IEduZimAPI.Models.Local;
using IEduZimAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;
        private readonly IEduContext _eduContext;
        private readonly IPaynowService _paynowService;

        public PaymentRepository(AppDbContext context, IEduContext eduContext, IPaynowService paynowService)
        {
            _context = context;
            _eduContext = eduContext;
            _paynowService = paynowService;
        }

        public async Task<Result<Payment>> AddAsync(Payment payment)
        {
            var student = await _eduContext.Students.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == payment.StudentId);

            switch (payment.PaymentMethod)
            {
                case PaymentMethod.ecocash:
                case PaymentMethod.onemoney:
                    var paynowPayment = _paynowService.CreatePaymentAsync(new PaynowPaymentRequest
                    {
                        AccountNumber = payment.AccountNumber,
                        Amount = payment.Amount,
                        Description = payment.Description,
                        Email = student.User.Email,
                        PaymentMethod = payment.PaymentMethod,
                        Reference = payment.Reference
                    }).Result;

                    payment.PollUrl = paynowPayment.Data.PollUrl;
                    payment.PaymentStatus = PaymentStatus.Initiated;
                    break;
                default:
                    payment.PaymentStatus = PaymentStatus.Initiated;
                    break;
            }

            payment.DateModified = DateTime.Now;
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();

            return new Result<Payment>(payment);
        }

        public async Task<Result<PaymentStatusResponse>> GetStatusAsync(string refNumber)
        {
            var payment = await _context.Payments.Where(x => x.Reference == refNumber).FirstOrDefaultAsync();

            var paynowResponse = await _paynowService.CheckPaymentStatusAsync(payment.PollUrl);
            string description = "";
            string status = "";

            if (paynowResponse.Contains("Sent") || paynowResponse.Contains("Created"))
            {
                description = "Transaction is awaiting approval/payment from customer.";
                status = PaymentStatus.Initiated.ToString();
            }
            else if (paynowResponse.Contains("Cancelled"))
            {
                description = "Transaction failed or was cancelled by customer.";
                status = PaymentStatus.Failed.ToString();

                payment.PaymentStatus = PaymentStatus.Failed;
            }
            else if (paynowResponse.Contains("Paid"))
            {
                status = PaymentStatus.Success.ToString();
                description = "Transaction was successfully paid by customer.";

                payment.PaymentStatus = PaymentStatus.Success;

                var subscription = await _context.Subscriptions.Where(x => x.PaymentId == payment.Id).FirstOrDefaultAsync();

                var lessonSchedules = await _context.LessonSchedules.Where(x => x.SubscriptionId == subscription.Id).ToListAsync();

                lessonSchedules.ForEach(x => x.Status = true);
                _context.LessonSchedules.UpdateRange(lessonSchedules);

                subscription.DateModified = DateTime.Now;
                subscription.Active = true;
                _context.Subscriptions.Update(subscription);
            }

            payment.DateModified = DateTime.Now;
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();

            PaymentStatusResponse response = new()
            {
                Description = description,
                PaymentStatus = status
            };

            return new Result<PaymentStatusResponse>(response);
        }
    }
}
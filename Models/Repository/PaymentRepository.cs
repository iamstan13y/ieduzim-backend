using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Enums;
using IEduZimAPI.Models.Local;
using IEduZimAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
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
            _paynowService = paynowService;
            _eduContext = eduContext;
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
                    payment.PaymentStatus = paynowPayment.Data.Success ? PaymentStatus.Success : PaymentStatus.Failed;
                    break;
                default:
                    payment.PaymentStatus = PaymentStatus.Success;
                    break;
            }

            payment.DateModified = DateTime.Now;
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            
            return new Result<Payment>(payment);
        }
    }
}
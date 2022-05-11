using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Enums;
using IEduZimAPI.Models.Local;
using IEduZimAPI.Models.Repository;
using IEduZimAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PaymentsController : Controller
    {
        private readonly StudentService service;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public PaymentsController(IPaymentRepository paymentRepository, ISubscriptionRepository subscriptionRepository)
        {
            service = new StudentService();
            _paymentRepository = paymentRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> PaySubscription(SubscriptionRequest request)
        {
            var student = service.GetByUserId(request.UserId);
            var payment = await _paymentRepository.AddAsync(new Payment
            {
                AccountNumber = request.PhoneNumber,
                Amount = request.Amount,
                Description = "Lesson Subscription Payment",
                PaymentStatus = PaymentStatus.Initiated,
                Reference = $"IEZ{DateTime.Now.Year}.{DateTime.Now.Ticks.ToString()[^10..]}",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                StudentId = student.Id,
                PaymentMethod = request.PaymentMethod
            });

            if (!payment.Succeeded) return BadRequest(payment);

            var subscription = await _subscriptionRepository.AddAsync(new Subscription
            {
                StudentId = student.Id,
                PaymentId = payment.Data.Id,
                HoursRemaining = request.PaymentPeriod,
                LessonStructureId = request.LessonStructureId,
                DateModified= DateTime.Now,
                DateCreated = DateTime.Now
            });

            if(!subscription.Succeeded) return BadRequest(subscription);

            return Ok(subscription);
        }

        [HttpGet("payment-status/{referenceNumber}")]
        public async Task<IActionResult> GetPaymentStatus(string referenceNumber)
        {
            var result = await _paymentRepository.GetStatusAsync(referenceNumber);
            return Ok(result);
        }

        //[HttpGet]
        //[Route("paged")]
        //public Pagination<Paginator<Payment>> GetPaged([FromQuery] PageRequest request) =>
        //  PagedExecution<Paginator<Payment>>.Execute(() => service.GetPaged(request));

        //[HttpGet]
        //[Route("paged/by-user-id/{userId}")]
        //public Pagination<Paginator<Payment>> GetPagedByUser([FromQuery] PageRequest request, string userId) =>
        // PagedExecution<Paginator<Payment>>.Execute(() => service.GetPagedByUser(request, userId));

        //[HttpGet]
        //[Route("paged/by-reference/{referenceNumber}")]
        //public Result<Payment> GetPagedByReference(string referenceNumber) =>
        // ExecutionService<Payment>.Execute(() => service.GetByReferenceNumber(referenceNumber));

        //[HttpPut]
        //[Route("status/{status}/{referenceNumber}")]
        //public Result<Payment> UpdateStatus(string status, string referenceNumber) =>
        // ExecutionService<Payment>.Execute(() => service.UpdateStatus(status,referenceNumber));
    }
}

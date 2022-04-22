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
        //private new PaymentsService service;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public PaymentsController(IPaymentRepository paymentRepository, ISubscriptionRepository subscriptionRepository)
        {
            //this.service = service;
            _paymentRepository = paymentRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> PaySubscription(SubscriptionRequest request)
        {
            var payment = await _paymentRepository.AddAsync(new Payment
            {
                AccountNumber = request.PhoneNumber,
                Amount = request.Amount,
                Description = "Lesson Subscription Payment",
                PaymentStatus = PaymentStatus.Initiated,
                Reference = $"IEZ{DateTime.Now.ToShortTimeString()}.{DateTime.Now.Ticks}",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                StudentId = request.StudentId,
                PaymentMethod = request.PaymentMethod
            });

            if (!payment.Succeeded) return BadRequest(payment);

            var subscription = await _subscriptionRepository.AddAsync(new Subscription
            {
                StudentId = request.StudentId,
                PaymentId = payment.Data.Id,
                HoursRemaining = request.PaymentPeriod,
                SubjectId = request.SubjectId,
                DateCreated = DateTime.Now,
            });

            if(!subscription.Succeeded) return BadRequest(subscription);

            return Ok(subscription);
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

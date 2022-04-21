using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Webdev.Payments;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PaymentsController : Controller
    {
        private new PaymentsService service;
        public PaymentsController() =>
            service = new PaymentsService();
        //[HttpPost]
        //[Route("subscribe")]
        //public Result<Payments> PaySubscription(Models.Local.Subscription subscription)
        //{
        //    var paynow = new Paynow(Startup.configuration["Payment:IntegrationId"], Startup.configuration["Payment:IntegrationKey"]);
        //    var payment = paynow.CreatePayment(DateTime.Now.Ticks.ToString(), Startup.configuration["Payment:Email"]);
        //    payment.Add(subscription.Title, subscription.Amount);
        //    var result = paynow.SendMobile(payment, subscription.PhoneNumber, subscription.PaymentMethod);
        //    if (!result.Success()) throw new Exception(result.Errors());
        //    return ExecutionService<Payments>.Execute(() => service.AddPayment(new Models.Local.Payment
        //    {
        //        AccountNumber = subscription.PhoneNumber,
        //        Payer = subscription.Payer,
        //        PaymentMethod = subscription.PaymentMethod,
        //        PollUrl = result.PollUrl(),
        //        Status = result.Success() ? "Success" : "Failed",
        //        UserId = subscription.UserId,
        //        Amount = subscription.Amount,
        //        Currency = subscription.Currency,
        //        Description = subscription.Title
        //    }, subscription.Payer, payment.Reference, subscription.PaymentPeriod, subscription.Currency, subscription.Email));
        //}

        [HttpGet]
        [Route("paged")]
        public Pagination<Paginator<Payments>> GetPaged([FromQuery] PageRequest request) =>
          PagedExecution<Paginator<Payments>>.Execute(() => service.GetPaged(request));

        [HttpGet]
        [Route("paged/by-user-id/{userId}")]
        public Pagination<Paginator<Payments>> GetPagedByUser([FromQuery] PageRequest request, string userId) =>
         PagedExecution<Paginator<Payments>>.Execute(() => service.GetPagedByUser(request, userId));

        [HttpGet]
        [Route("paged/by-reference/{referenceNumber}")]
        public Result<Payments> GetPagedByReference(string referenceNumber) =>
         ExecutionService<Payments>.Execute(() => service.GetByReferenceNumber(referenceNumber));

        [HttpPut]
        [Route("status/{status}/{referenceNumber}")]
        public Result<Payments> UpdateStatus(string status, string referenceNumber) =>
         ExecutionService<Payments>.Execute(() => service.UpdateStatus(status,referenceNumber));
    }
}

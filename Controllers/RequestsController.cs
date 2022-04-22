//using IEduZimAPI.CoreClasses;
//using IEduZimAPI.Models.Data;
//using IEduZimAPI.Services;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using Webdev.Payments;

//namespace IEduZimAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    //[Authorize]
//    public class RequestsController : ControllerBase
//    {
//        private new RequestsService service;
//        public RequestsController() =>
//            service = new RequestsService();

//        [HttpPost]
//        [Route("add")]
//        public Result<Payments> PaySubscription(Models.Local.LessonRequest subscription)
//        {
//            var paynow = new Paynow(Startup.configuration["Payment:IntegrationId"], Startup.configuration["Payment:IntegrationKey"]);
//            var payment = paynow.CreatePayment(DateTime.Now.Ticks.ToString(), Startup.configuration["Payment:Email"]);
//            payment.Add(subscription.Title, (decimal)subscription.Amount);
//            var result = paynow.SendMobile(payment, subscription.PhoneNumber, subscription.PaymentMethod);
//            if (!result.Success()) throw new Exception(result.Errors());
//            return ExecutionService<Payments>.Execute(() => service.AddPayment(new Models.Local.Payment
//            {
//                AccountNumber = subscription.PhoneNumber,
//                Payer = subscription.Payer,
//                PaymentMethod = subscription.PaymentMethod,
//                PollUrl = result.PollUrl(),
//                Status = result.Success() ? "Success" : "Failed",
//                UserId = subscription.UserId,
//                Amount = subscription.Amount,
//                Currency = subscription.Currency,
//                Description = subscription.Title
//            }, subscription.Payer, payment.Reference, subscription.PaymentPeriodId.ToString(), subscription.Currency, subscription.Email, subscription));
//        }

//        [HttpGet]
//        [Route("pending-by-teacher/{teacherId}")]
//        public Pagination<Paginator<LessonRequests>> GetPendingByTeacher([FromQuery] PageRequest request, int teacherId) =>
//          PagedExecution<Paginator<LessonRequests>>.Execute(() => service.GetPendingRequestsByTeacherId(request, teacherId));

//        [HttpGet]
//        [Route("previous-by-teacher/{teacherId}")]
//        public Pagination<Paginator<LessonRequests>> GetPreviousByTeacher([FromQuery] PageRequest request, int teacherId) =>
//          PagedExecution<Paginator<LessonRequests>>.Execute(() => service.GetPreviousRequestsByTeacherId(request, teacherId));
//    }
//}

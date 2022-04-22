//using IEduZimAPI.CoreClasses;
//using IEduZimAPI.Models.Data;
//using IEduZimAPI.Service;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace IEduZimAPI.Services
//{
//    public class RequestsService : BaseService<LessonRequests>
//    {

//        public Payments AddPayment(Models.Local.Payment payment, string user, string refNumber, string paymentPeriod, string currency, string email, Models.Local.LessonRequest request)
//        {
//            var payments = new Payments
//            {
//                AccountNumber = payment.AccountNumber,
//                DateCreated = DateTime.Now,
//                DateModified = DateTime.Now,
//                ModifiedBy = user,
//                Payer = payment.Payer,
//                PaymentMethod = payment.PaymentMethod,
//                PollUrl = payment.PollUrl,
//                ReferenceNumber = refNumber,
//                Status = payment.Status,
//                UserId = payment.UserId,
//                Amount = payment.Amount
//            };
//            context.Payments.Add(payments);
//            var pmnt = GetByReferenceNumber(refNumber);
//            context.LessonRequests.Add(new LessonRequests
//            {
//                Accepted = true,
//                Administered = false,
//                Amount = payment.Amount,
//                UserId = payment.UserId,
//                EndDate = request.EndDate,
//                StartDate = request.StartDate,
//                ExamTypeId = request.ExamTypeId,
//                PaymentPeriodId = request.PaymentPeriodId,
//                Subject = request.Subject,
//                Teacher = request.Teacher,
//                TeacherId = request.TeacherId,
//                Paid = true,
//                PaymentId = pmnt.Id,
//                LessonLocationId = request.LessonLocationId
//            });
//            SendSubsriptionEmail(payments, paymentPeriod, currency, email);
//            context.SaveChanges();
//            return pmnt;
//        }

//        private void SendSubsriptionEmail(Payments payment, string paymentPeriod, string currency, string email)
//        {
//            var body = new Dictionary<string, string>
//            {
//                {"{user}", payment.Payer },
//                {"{paymentperiod}", paymentPeriod } ,
//                {"{currency}", currency},
//                {"{amount}", payment.Amount.ToString() },
//                {"{subject}", "Payment Confirmation"},
//                {"{referenceNumber}", payment.ReferenceNumber},
//                {"{emailAddress}", email }
//            };
//            EmailService.Send(Models.Enums.EmailType.StudentPayments, body);
//        }

//        public LessonRequests UpdateAdministeredRequest(int requestId, bool administered)
//        {
//            var request = context.LessonRequests.FirstOrDefault(a => a.Id == requestId);
//            request.Administered = administered;
//            context.SaveChanges();
//            return request;
//        }

//        public Paginator<LessonRequests> GetPendingRequestsByTeacherId(PageRequest request, int teacherId)
//        {
//            if (request == null) request = new PageRequest() { PageNumber = 1, PageSize = 10 };
//            var req = context.LessonRequests.Where(a => a.TeacherId == teacherId && DateTime.Compare(a.EndDate, DateTime.Now) > 0).OrderByDescending(a=> a.StartDate).AsQueryable();
//            var total = req.CountAsync().Result;
//            req = req.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
//            return new Paginator<LessonRequests>(request, total, req);
//        }

//        public Paginator<LessonRequests> GetPreviousRequestsByTeacherId(PageRequest request, int teacherId)
//        {
//            if (request == null) request = new PageRequest() { PageNumber = 1, PageSize = 10 };
//            var req = context.LessonRequests.Where(a => a.TeacherId == teacherId && DateTime.Compare(a.EndDate, DateTime.Now) < 0).OrderByDescending(a => a.StartDate).AsQueryable();
//            var total = req.CountAsync().Result;
//            req = req.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
//            return new Paginator<LessonRequests>(request, total, req);
//        }



//        public Payments GetByReferenceNumber(string referenceNumber) =>
//            context.Payments.OrderByDescending(x => x.DateCreated).LastOrDefault(a => a.ReferenceNumber == referenceNumber);
//    }
//}

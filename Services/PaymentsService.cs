//using IEduZimAPI.CoreClasses;
//using IEduZimAPI.Models;
//using IEduZimAPI.Models.Data;
//using IEduZimAPI.Service;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace IEduZimAPI.Services
//{
//    public class PaymentsService:BaseService<Payment>
//    {
        
//        public Paginator<Payment> GetPagedByUser(PageRequest request, string userId)
//        {
//            if (request == null) request = new PageRequest() { PageNumber = 1, PageSize = 10 };
//            var req = context.Payments.Where(w=> w.UserId == userId).Include(b => b.User).OrderByDescending(a => a.DateCreated).AsQueryable();

//            var total = req.CountAsync().Result;
//            req = req.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
//            return new Paginator<Payment>(request, total, req);
//        }

//        public Payments GetByReferenceNumber(string referenceNumber) =>
//            context.Payments.OrderByDescending(x=> x.DateCreated).LastOrDefault(a => a.ReferenceNumber == referenceNumber);

//        public Payments AddPayment(Models.Local.Payment payment, string user, string refNumber, string paymentPeriod, string currency, string email)
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
//            var teacher = context.Teachers.FirstOrDefault(a => a.UserId == payment.UserId);
//            if (teacher != null)
//                teacher.Subscribed = true;
//            SendSubsriptionEmail(payments, paymentPeriod, currency, email);
//            context.SaveChanges();
//            return GetByReferenceNumber(refNumber);
//        }

//        public Payments UpdateStatus(string status, string referenceNumber)
//        {
//            var payment = GetByReferenceNumber(referenceNumber);
//            payment.Status = status;
//            context.SaveChanges();
//            return payment;
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
//            EmailService.Send(Models.Enums.EmailType.TeacherSubscription, body);
//        }
//    }
//}

//using IEduZimAPI.CoreClasses;
//using IEduZimAPI.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace IEduZimAPI.Services
//{
//    public class TeacherDocumentsService : BaseService<TeacherDocuments>
//    { 
//        public IEnumerable<TeacherDocuments> GetByUserId(string userId) =>
//            context.TeacherDocuments.Include(i => i.User).Include(u => u.QualificationDocuments).Where(a => a.UserId == userId);

//        public TeacherDocuments AuthenticateDocument(string UserId, int DocumentId)
//        {
//            var document = context.TeacherDocuments.FirstOrDefault(a => a.UserId == UserId && a.Id == DocumentId);

//            if (document == null) throw new Exception("Requested Document for this User could not be found");
//            var payment = context.Payments.FirstOrDefault(a => a.UserId == UserId);
//            if (payment == null) throw new Exception("Teachers must subscribe first before you verify their documents.");
//            document.Authenticated = !document.Authenticated;
//            context.TeacherDocuments.Update(document);
//            context.SaveChanges();
//            CheckAllAuthenticated(UserId);
//            return document;
//        }

//        public bool CheckAllAuthenticated(string userId)
//        {
//            var all = context.TeacherDocuments.Where(a => a.UserId == userId);
//            var unauthenticated = all.Where(b => b.Authenticated != true);
//            var teacher = context.Teachers.FirstOrDefault(a => a.UserId == userId);
//            if (all.Count() > 0 && unauthenticated.Count() == 0)
//            {
//                teacher.Verified = true;
//                context.SaveChanges();
//                return true;
//            }
//            else
//            {
//                teacher.Verified = false;
//                context.SaveChanges();
//            }

//            return false;
//        }
//    }
//}

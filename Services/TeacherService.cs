using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace IEduZimAPI.Services
{
    public class TeacherService: BaseService<Teacher>
    {
        private UserManager<IdentityUser> userManager;
        public TeacherService(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.AddTeacher();
        }

        public Teacher GetByUserId(string userId) =>
            context.Teachers.Include(i=> i.Title).Include(u=> u.User).FirstOrDefault(a => a.UserId == userId);

        public override Teacher Update<TKey>(TKey id,Teacher item, string user)
        {
            var u = userManager.FindByIdAsync(item.UserId).Result;
            u.PhoneNumber = item.PhoneNumber;
            userManager.UpdateAsync(u);
            return base.Update(id ,item, user);
        }


        public Teacher GetDefault() => context.Teachers.FirstOrDefault(a => a.Name == "Default"  && a.Surname == "User");

        public void AddTeacher()
        {
            var teacher = context.Teachers.FirstOrDefault(a => a.Name == "Default" && a.Surname == "User");
            if (teacher == null)
            {
                context.Teachers.Add(new Teacher
                {
                    Name = "Default",
                    Gender = "Other",
                    PhoneNumber = "00000000",
                    Surname = "User",
                    TitleId = context.Titles.FirstOrDefault().Id,
                    Verified = false
                });
                context.SaveChanges();
            }
        }

        public Paginator<Teacher> GetVerified(PageRequest request)
        {
            if (request == null) request = new PageRequest() { PageNumber = 1, PageSize = 10 };
            var req = context.Teachers.Where(a => a.Verified == true && a.Subscribed == true).Include(b => b.Title).Include(c => c.User).Include(d => d.User).AsQueryable();
            var total = req.CountAsync().Result;
            req = req.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
            return new Paginator<Teacher>(request, total, req);
        }

        public Paginator<Teacher> GetSubscribedUnverified(PageRequest request)
        {
            if (request == null) request = new PageRequest() { PageNumber = 1, PageSize = 10 };
            var req = context.Teachers.Where(a => a.Verified == false && a.Subscribed == true).Include(b => b.Title).Include(c => c.User).Include(d => d.User).AsQueryable();
            var total = req.CountAsync().Result;
            req = req.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
            return new Paginator<Teacher>(request, total, req);
        }

        public Paginator<Teacher> GetUnsubcribed(PageRequest request)
        {
            if (request == null) request = new PageRequest() { PageNumber = 1, PageSize = 10 };
            var req = context.Teachers.Where(a => a.Verified == false && a.Subscribed == false && a.UserId != null).Include(b => b.Title).Include(c => c.User).Include(d => d.User).AsQueryable();
            var total = req.CountAsync().Result;
            req = req.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
            return new Paginator<Teacher>(request, total, req);
        }

        public Teacher UpdateSubscriptionStatus(Teacher teacher, string userId)
        {
            var payment = context.Payments.FirstOrDefault(a => a.UserId == userId);
            if (payment == null) throw new Exception("An Initial Payment has to be made first to mark teacher as subscribed.");
            return base.Update(teacher.Id, teacher, null);
        }

    }
}

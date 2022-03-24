using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IEduZimAPI.Services
{
    public class StudentService : BaseService<Student>
    {
        public Student GetByUserId(string userId) =>
            context.Students.Include(i => i.Title).Include(u => u.User).FirstOrDefault(a => a.UserId == userId);
    }
}

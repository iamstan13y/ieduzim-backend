using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System.Linq;

namespace IEduZimAPI.Services
{
    public class TeacherActiveDaysService : BaseService<TeacherActiveDays>
    {
        public TeacherActiveDays GetByUserId(string userId) =>
          context.TeacherActiveDays.FirstOrDefault(i => i.UserId == userId);
    }
}

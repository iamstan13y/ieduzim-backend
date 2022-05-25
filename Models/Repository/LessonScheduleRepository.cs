using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class LessonScheduleRepository : ILessonScheduleRepository
    {
        public Task<Result<IEnumerable<LessonSchedule>>> AddAsync(LessonScheduleRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<IEnumerable<LessonSchedule>>> GetByLessonLocationIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<IEnumerable<LessonSchedule>>> GetByUserIdAsync(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
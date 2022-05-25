using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface ILessonScheduleRepository
    {
        Task<Result<IEnumerable<LessonSchedule>>> AddAsync(LessonScheduleRequest request);
        Task<Result<IEnumerable<LessonSchedule>>> GetByTeacherIdAsync(int teacherId);
        Task<Result<IEnumerable<LessonSchedule>>> GetByLessonLocationIdAsync(int id);
    }
}
using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface ILessonScheduleRepository
    {
        Result<IEnumerable<LessonSchedule>> Add(LessonScheduleRequest request);
        Task<Result<IEnumerable<LessonSchedule>>> GetByTeacherIdAsync(int teacherId);
        Task<Result<IEnumerable<LessonSchedule>>> GetByLessonLocationIdAsync(int id);
        Task<Result<IEnumerable<LocalAddress>>> GetByCriteriaAsync(AddressSearchRequest request);
        Task<Result<IEnumerable<LessonDay>>> GetDaysAsync();
    }
}
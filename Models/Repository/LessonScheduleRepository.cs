using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class LessonScheduleRepository : ILessonScheduleRepository
    {
        private readonly AppDbContext _context;

        public LessonScheduleRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<Result<IEnumerable<LessonSchedule>>> AddAsync(LessonScheduleRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<IEnumerable<LessonSchedule>>> GetByLessonLocationIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Result<IEnumerable<LessonSchedule>>> GetByTeacherIdAsync(int teacherId)
        {
            var schedules = await _context.LessonSchedules.Where(x => x.LessonStructure.TeacherId == teacherId).ToListAsync();
            return new Result<IEnumerable<LessonSchedule>>(schedules);
        }
    }
}
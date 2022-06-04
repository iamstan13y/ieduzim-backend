using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Enums;
using IEduZimAPI.Models.Local;
using Microsoft.EntityFrameworkCore;
using System;
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

        public Result<IEnumerable<LessonSchedule>> Add(LessonScheduleRequest request)
        {
            if (request.LessonDays.Contains(1) && request.LessonDays.Count > 1)
                return new Result<IEnumerable<LessonSchedule>>(false, "You cannot make other selections with Everyday!", null);

            LessonSchedule schedule = null;

            request.LessonDays.ForEach(x =>
            {
                var scheduleInDb = _context.LessonSchedules.Where(y => y.LessonDay == x && request.StartTime >= y.StartTime && request.EndTime <= y.EndTime).FirstOrDefault();
                if (scheduleInDb != null) schedule = scheduleInDb;
            });

            if (schedule != null) return new Result<IEnumerable<LessonSchedule>>(false, "Teacher is occupied in provided time.", null);

            request.LessonDays.ForEach(x =>
            {
                _context.LessonSchedules.Add(new LessonSchedule
                {
                    LessonDay = x,
                    LessonStructureId = request.LessonStructureId,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    StudentId = request.StudentId
                });

               _context.SaveChanges();
            });

            return new Result<IEnumerable<LessonSchedule>>();
        }

        public async Task<Result<IEnumerable<LocalAddress>>> GetByCriteriaAsync(AddressSearchRequest request)
        {
            if (request.LessonDays.Contains(1) && request.LessonDays.Count > 1)
                return new Result<IEnumerable<LocalAddress>>(false, "You cannot make other selections with Everyday!", null);

            var student = await _context.Students.Where(x => x.UserId == request.UserId).FirstOrDefaultAsync();
            if (student == null) return new Result<IEnumerable<LocalAddress>>(false, "Student not found", null);

            var studentLocation = await _context.Locations.Where(x => x.Id == student.LocationId).FirstOrDefaultAsync();

            var addresses = await _context.Addresses
                .Where(x => x.Location.CityId == studentLocation.CityId && x.Location.Distance <= 10)
                .OrderBy(x => x.Location.Distance)
                .Include(x => x.Location)
                .ToListAsync();

            addresses.ForEach(x =>
            {
                x.LessonStructure = _context.LessonStructures.Where(y => y.SubjectId == request.SubjectId).FirstOrDefault();
            });

            return new Result<IEnumerable<LocalAddress>>(addresses);
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

        public async Task<Result<IEnumerable<LessonDay>>> GetDaysAsync()
        {
            var days = await _context.LessonDays.ToListAsync();

            return new Result<IEnumerable<LessonDay>>(days);
        }
    }
}
using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class LessonRepository : ILessonRepository
    {
        private readonly AppDbContext _context;

        public LessonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<Lesson>>> AddAsync(LessonRequest lessonRequest)
        {
            List<Lesson> lessons = new();

            foreach (var subsId in lessonRequest.SubscriptionIds)
            {
                lessons.Add(new Lesson
                {
                    SubscriptionId = subsId,
                    LessonDate = lessonRequest.LessonDate,
                    StartTime = lessonRequest.StartTime,
                    EndTime = lessonRequest.EndTime
                });

                var duration = (lessonRequest.EndTime - lessonRequest.StartTime).TotalHours;
                var sub = await _context.Subscriptions.Where(x => x.Id == subsId).FirstOrDefaultAsync();
                sub.HoursRemaining -= (int)duration;
                _context.Subscriptions.Update(sub);
                await _context.SaveChangesAsync();
            }

            await _context.Lessons.AddRangeAsync(lessons);
            await _context.SaveChangesAsync();

            return new Result<IEnumerable<Lesson>>(lessons);
        }

        public Task<Result<IEnumerable<Lesson>>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<Lesson>> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Result<IEnumerable<Lesson>>> GetByStudentIdAsync(int id)
        {
            var lessons = await _context.Lessons
                .Include(x => x.Subscription)
                .Include(x => x.Subscription.Student)
                .Include(x => x.Subscription.LessonStructure)
                .Where(x => x.Subscription.StudentId == id)
                .ToListAsync();

            return new Result<IEnumerable<Lesson>>(lessons);
        }

        public async Task<Result<IEnumerable<Lesson>>> GetByTeacherIdAsync(int id)
        {
            var lessons = await _context.Lessons.Where(x => x.Subscription.LessonStructure.TeacherId == id).ToListAsync();

            return new Result<IEnumerable<Lesson>>(lessons);
        }

        public Task<Result<Lesson>> UpdateAsync(Lesson lesson)
        {
            throw new System.NotImplementedException();
        }
    }
}
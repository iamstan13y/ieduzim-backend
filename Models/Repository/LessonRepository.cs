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
                //sub.HoursRemaining -= (int)duration;
                _context.Subscriptions.Update(sub);
                await _context.SaveChangesAsync();
            }

            await _context.Lessons.AddRangeAsync(lessons);
            await _context.SaveChangesAsync();

            return new Result<IEnumerable<Lesson>>(lessons);
        }

        //rhetoric
        public async Task<Result<IEnumerable<Lesson>>> AddAsync(TeacherLessonRequest lesson)
        {
            List<Lesson> lessons = new();

            var subscriptions = await _context.Subscriptions.ToListAsync();
            foreach (var sub in subscriptions)
            {
                lessons.Add(new Lesson
                {
                    SubscriptionId = sub.Id,
                    LessonDate = lesson.LessonDate,
                    StartTime = lesson.StartTime,
                    EndTime = lesson.EndTime
                });

                var duration = (lesson.EndTime - lesson.StartTime).TotalHours;
                //var sub = await _context.Subscriptions.Where(x => x.Id == subsId).FirstOrDefaultAsync();
                //sub.HoursRemaining -= (int)duration;
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
                .Where(x => x.Subscription.StudentId == id)
                .ToListAsync();

            return new Result<IEnumerable<Lesson>>(lessons);
        }

        public async Task<Result<IEnumerable<Lesson>>> GetByTeacherIdAsync(int id)
        {
            var lessons = await _context.Lessons
                .Include(x => x.Subscription).ToListAsync();

            return new Result<IEnumerable<Lesson>>(lessons);
        }

        public async Task<Result<Lesson>> StudentUpdateAsync(UpdateLessonRequest request)
        {
            var lesson = await _context.Lessons.Where(x => x.SubscriptionId == request.SubscriptionIds[0]).FirstOrDefaultAsync();
            lesson.LessonStatus = request.LessonStatus;
            return new Result<Lesson>(lesson);
        }

        public async Task<Result<IEnumerable<Lesson>>> TeacherUpdateAsync(UpdateLessonRequest request)
        {
            List<Lesson> lessons = new();
            foreach (var sub in request.SubscriptionIds)
            {
                var lesson = await _context.Lessons.Where(x => x.SubscriptionId == sub).FirstOrDefaultAsync();
                lesson.LessonStatus = request.LessonStatus;
                await _context.SaveChangesAsync();
                lessons.Add(lesson);
            }

            return new Result<IEnumerable<Lesson>>(lessons);
        }
    }
}
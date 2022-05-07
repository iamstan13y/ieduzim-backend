using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using System.Collections.Generic;
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
                lessons.Add(new Lesson
                {
                    SubscriptionId = subsId,
                    StartDate = lessonRequest.StartDate,
                    EndDate = lessonRequest.EndDate,
                    Confirmed = lessonRequest.Confirmed
                });

            await _context.Lessons.AddRangeAsync(lessons);
            await _context.SaveChangesAsync();

            return new Result<IEnumerable<Lesson>>(lessons);
        }

        public Task<Result<IEnumerable<Lesson>>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<Lesson>> GetAllAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<Lesson>> UpdateAsync(Lesson lesson)
        {
            throw new System.NotImplementedException();
        }
    }
}
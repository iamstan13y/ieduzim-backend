using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class LessonLocationRepository : ILessonLocationRepository
    {
        private readonly AppDbContext _context;

        public LessonLocationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<LessonLocation>> AddAsync(LessonLocation lessonLocation)
        {
            await _context.AddAsync(lessonLocation);
            await _context.SaveChangesAsync();
            return new Result<LessonLocation>(lessonLocation);
        }

        public async Task<Result<IEnumerable<LessonLocation>>> GetAllAsync()
        {
            var lessonLocations = await _context.LessonLocations.ToListAsync();
            return new Result<IEnumerable<LessonLocation>>(lessonLocations);
        }

        public async Task<Result<LessonLocation>> GetByIdAsync(int id)
        {
            var lessonLocation = await _context.LessonLocations.FindAsync(id);
            return new Result<LessonLocation>(lessonLocation);
        }

        public Task<Result<LessonLocation>> UpdateAsync(LessonLocation lessonLocation)
        {
            throw new System.NotImplementedException();
        }
    }
}
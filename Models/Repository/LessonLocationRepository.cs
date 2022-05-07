using IEduZimAPI.CoreClasses;
using IEduZimAPI.Migrations.AppDb;
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

        public Task<Result<LessonLocation>> AddAsync(LessonLocation lessonLocation)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Result<IEnumerable<LessonLocation>>> GetAllAsync()
        {
            var lessonLocations = await _context.LessonLocations.ToListAsync();
            return new Result<IEnumerable<LessonLocation>>(lessonLocations);
        }

        public Task<Result<LessonLocation>> GetByIdAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<LessonLocation>> UpdateAsync(LessonLocation lessonLocation)
        {
            throw new System.NotImplementedException();
        }
    }
}
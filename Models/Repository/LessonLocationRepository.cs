using IEduZimAPI.CoreClasses;
using IEduZimAPI.Migrations.AppDb;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class LessonLocationRepository : ILessonLocationRepository
    {
        public Task<Result<LessonLocation>> AddAsync(LessonLocation lessonLocation)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<IEnumerable<LessonLocation>>> GetAllAsync()
        {
            throw new System.NotImplementedException();
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
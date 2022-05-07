using IEduZimAPI.CoreClasses;
using IEduZimAPI.Migrations.AppDb;
using IEduZimAPI.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface ILessonLocationRepository
    {
        Task<Result<LessonLocation>> AddAsync(LessonLocation lessonLocation);
        Task<Result<LessonLocation>> UpdateAsync(LessonLocation lessonLocation);
        Task<Result<IEnumerable<LessonLocation>>> GetAllAsync();
        Task<Result<LessonLocation>> GetByIdAsync(int id);
    }
}
using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface ILessonRepository
    {
        Task<Result<Lesson>> AddAsync(Lesson lesson);
        Task<Result<Lesson>> UpdateAsync(Lesson lesson);
        Task<Result<IEnumerable<Lesson>>> GetAllAsync();
        Task<Result<Lesson>> GetAllAsync(int id);
    }
}
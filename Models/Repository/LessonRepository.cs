using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class LessonRepository : ILessonRepository
    {
        public Task<Result<Lesson>> AddAsync(Lesson lesson)
        {
            throw new System.NotImplementedException();
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
using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface ILessonRepository
    {
        Task<Result<IEnumerable<Lesson>>> AddAsync(LessonRequest lesson);
        Task<Result<IEnumerable<Lesson>>> TeacherUpdateAsync(UpdateLessonRequest request);
        Task<Result<IEnumerable<Lesson>>> GetAllAsync();
        Task<Result<Lesson>> GetByIdAsync(int id);
        Task<Result<IEnumerable<Lesson>>> GetByStudentIdAsync(int id);
        Task<Result<IEnumerable<Lesson>>> GetByTeacherIdAsync(int id);
    }
}
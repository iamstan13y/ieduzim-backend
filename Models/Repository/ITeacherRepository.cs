using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface ITeacherRepository
    {
        Task<Result<Teacher>> AddAsync(Teacher teacher);
    }
}
using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        public Task<Result<Teacher>> AddAsync(Teacher teacher)
        {
            throw new System.NotImplementedException();
        }
    }
}
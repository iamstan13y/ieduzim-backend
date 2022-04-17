using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface ISubjectRepository
    {
        Task<Result<IEnumerable<Subject>>> GetAllSubjectsAsync();
    }
}
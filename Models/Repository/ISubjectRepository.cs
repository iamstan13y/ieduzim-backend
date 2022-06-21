using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface ISubjectRepository
    {
        Task<Result<IEnumerable<Subject>>> GetAllSubjectsAsync();
        Task<Paginator<Subject>> GetAllSubjectsPagedAsync(PageRequest request);
        Task<Result<Subject>> AddAsync(HubSubjectRequest request);
        Task<Result<IEnumerable<Subject>>> GetPageByCriteriaAsync(int levelId, int lessonLocationId);
    }
}
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
        Task<Result<Subject>> AddAsync(SubjectRequest request);
        Task<Result<Subject>> AddToHubAsync(HubSubjectRequest request);
        Task<Result<IEnumerable<Subject>>> GetPageByCriteriaAsync(int levelId, int lessonLocationId);
        Task<Result<IEnumerable<Subject>>> GetHubSubjectsAsync(string userId, int levelId, int lessonLocationId);
    }
}
using IEduZimAPI.CoreClasses;
using IEduZimAPI.CoreClasses.Pagination;
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
        Task<Result<Subject>> AddAsync(Subject subject);
        Task<Result<Pageable<Subject>>> GetPageByCriteriaAsync(SearchSubjectRequest request, Pagination pagination);
    }
}
using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Mvc;
using IEduZimAPI.Services;
using System.Collections.Generic;
using IEduZimAPI.Models.Repository;
using System.Threading.Tasks;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SubjectsController : BaseController<Subject, Models.Local.Subjects>
    {
        private SubjectsService service;
        private readonly ISubjectRepository _subjectRepository;
        public SubjectsController(ISubjectRepository subjectRepository)
        {
            service = new SubjectsService();
            _subjectRepository = subjectRepository;
        }


        [HttpGet]
        [Route("paged/by-level/{levelId}")]
        public virtual Pagination<Paginator<Subject>> GetPaged([FromQuery] PageRequest request, int levelId) =>
           PagedExecution<Paginator<Subject>>.Execute(() => service.GetPagedByLevel(request, levelId));

        [HttpGet]
        public override Result<IEnumerable<Subject>> Get() => _subjectRepository.GetAllSubjectsAsync().Result;
    }
}
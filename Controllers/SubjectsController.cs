using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Mvc;
using IEduZimAPI.Services;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SubjectsController : BaseController<Subject, Models.Local.Subjects>
    {
        private SubjectsService service;
        public SubjectsController() => service = new SubjectsService();

        [HttpGet]
        [Route("paged/by-level/{levelId}")]
        public virtual Pagination<Paginator<Subject>> GetPaged([FromQuery]PageRequest request, int levelId) =>
           PagedExecution<Paginator<Subject>>.Execute(() => service.GetPagedByLevel(request, levelId));
    }
}
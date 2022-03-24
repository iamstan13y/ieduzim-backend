using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LessonStructureController : BaseController<LessonStructure, Models.Local.LocalLessonStructure>
    {
        private LessonStructureService service;
        public LessonStructureController() => service = new LessonStructureService();

        [HttpGet]
        [Route("paged/by-userId/{userId}")]
        public virtual Pagination<Paginator<LessonStructure>> GetPagedByUser([FromQuery] PageRequest request, string userId) =>
           PagedExecution<Paginator<LessonStructure>>.Execute(() => service.GetPagedByUserId(request, userId));

    }
}

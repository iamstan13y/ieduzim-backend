using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Repository;
using IEduZimAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LessonStructureController : BaseController<LessonStructure, Models.Local.LocalLessonStructure>
    {
        private readonly ILessonStructureRepository _lessonStructureRepository;

        public LessonStructureController(ILessonStructureRepository lessonStructureRepository)
        {
            _lessonStructureRepository = lessonStructureRepository;
        }

        [HttpGet]
        [Route("paged/by-userId/{userId}")]
        public virtual Pagination<Paginator<LessonStructure>> GetPagedByUser([FromQuery] PageRequest request, string userId) =>
           PagedExecution<Paginator<LessonStructure>>.Execute(() => _lessonStructureRepository.GetPagedByUserId(request, userId).Result);

    }
}

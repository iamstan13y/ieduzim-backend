using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LessonLocationsController : BaseController<LessonLocationsData, Models.Local.LessonLocation>
    {
        public LessonLocationsController() : base(new BaseService<LessonLocationsData>()) { }
    }
}

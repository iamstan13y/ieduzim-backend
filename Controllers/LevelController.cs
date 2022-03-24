using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LevelController : BaseController<Level, Models.Local.BaseName>
    {
        public LevelController() : base(new BaseService<Level>()) { }
    }
}
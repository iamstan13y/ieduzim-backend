using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TitlesController : BaseController<Title, Models.Local.BaseName>
    {
        public TitlesController() : base(new BaseService<Title>()) { }
    }
}
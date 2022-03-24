using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProvincesController : BaseController<Province, Models.Local.BaseName>
    {
        public ProvincesController() : base(new BaseService<Province>()) { }
    }
}
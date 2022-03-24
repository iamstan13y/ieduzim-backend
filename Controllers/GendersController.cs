using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class GendersController : BaseController<Gender, Models.Local.BaseName>
    {
        public GendersController() : base(new BaseService<Gender>()) { }
    }
    
}

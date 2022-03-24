using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CitiesController : BaseController<City, Models.Local.BaseName>
    {
        public CitiesController() : base(new BaseService<City>()) { }
    }
}
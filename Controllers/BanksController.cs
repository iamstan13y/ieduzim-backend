using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BanksController : BaseController<Banks, Models.Local.BaseName>
    {
        public BanksController() : base(new BaseService<Banks>()) { }
    }
}
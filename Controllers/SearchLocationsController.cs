using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using IEduZimAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SearchLocationsController : Controller
    {
        private new SearchLocationsService service;
        public SearchLocationsController(IEduContext context, AppDbContext appDbContext) =>
            service = new SearchLocationsService(context, appDbContext);

        [HttpGet]
        [Route("search")]
        public Result<Paginator<Address>> Get([FromQuery]LocationSearchRequest request) =>
            ExecutionService<Paginator<Address>>.Execute(() => service.GetPagedLocationsByCriteria(request));
    }
}

using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DistancePricesController : BaseController<DistancePrices, Models.Local.DistancePrice>
    {
        private new DistanceCostsService service;
        public DistancePricesController() =>
            service = new DistanceCostsService();

        [HttpGet]
        [Route("paged-costs")]
        public override Pagination<Paginator<DistancePrices>> GetPaged([FromQuery] PageRequest request) =>
          PagedExecution<Paginator<DistancePrices>>.Execute(() => service.GetPaged(request));
    }
}

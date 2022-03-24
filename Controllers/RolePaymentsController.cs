using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RolePaymentsController : BaseController<RolePaymentsSettings, Models.Local.RolePaymentSetting>
    {
        private new RolePaymentPeriodsService service;
        public RolePaymentsController() =>
            service = new RolePaymentPeriodsService();

        [HttpGet]
        [Route("all")]
        public Result<Paginator<RolePaymentsSettings>> Get([FromQuery] PageRequest request) =>
           ExecutionService<Paginator<RolePaymentsSettings>>.Execute(() => service.GetPaged(request));

    }
}
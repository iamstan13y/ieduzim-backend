using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PaymentPeriodsController : BaseController<PaymentPeriods, Models.Local.PaymentPeriod>
    {
        public PaymentPeriodsController() : base(new BaseService<PaymentPeriods>()) { }
    }
}
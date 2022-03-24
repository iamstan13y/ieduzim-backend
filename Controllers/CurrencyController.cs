using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CurrencyController : BaseController<Currencies, Models.Local.Currency>
    {
        public CurrencyController() : base(new BaseService<Currencies>()) { }
    }
}
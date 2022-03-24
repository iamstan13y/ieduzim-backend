using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BanksDetailsController : BaseController<BankDetails, Models.Local.BankDetail>
    {
        private new BankDetailsService service;
        public BanksDetailsController() =>
            service = new BankDetailsService();

        [HttpGet]
        [Route("by-user-id/{userId}")]
        public Result<IEnumerable<BankDetails>> Get(string userId) =>
            ExecutionService<IEnumerable<BankDetails>>.Execute(() => service.GetByUserId(userId));
    }
}

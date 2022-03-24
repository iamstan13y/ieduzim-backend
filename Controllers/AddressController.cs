using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AddressController : BaseController<Address, Models.Local.LocalAddress>
    {
        private new AddressService service;
        public AddressController() =>
            service = new AddressService();

        [HttpGet]
        [Route("by-user-id/{userId}")]
        public Result<Address> Get(string userId) =>
            ExecutionService<Address>.Execute(() => service.GetByUserId(userId));
    }
}

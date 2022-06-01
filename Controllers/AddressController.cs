using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using IEduZimAPI.Models.Repository;
using IEduZimAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AddressController : ControllerBase //: BaseController<Address, Models.Local.LocalAddress>
    {
        private readonly IAddressRepository _addressRepository;

        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<IActionResult> Get(AddressSearchRequest request)
        {
            var result = await _addressRepository.GetByCriteriaAsync(request);
            return Ok(result);
        }

        //[HttpGet]
        //[Route("by-user-id/{userId}")]
        //public Result<Address> Get(string userId) =>
        //    ExecutionService<Address>.Execute(() => service.GetByUserId(userId));
    }
}

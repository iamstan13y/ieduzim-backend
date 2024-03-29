﻿using IEduZimAPI.Models.Local;
using IEduZimAPI.Models.Repository;
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

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] AddressSearchRequest request)
        {
            var result = await _addressRepository.GetByCriteriaAsync(request);

            if (!result.Succeeded) return BadRequest(result);

            return Ok(result);
        }

        //[HttpGet]
        //[Route("by-user-id/{userId}")]
        //public Result<Address> Get(string userId) =>
        //    ExecutionService<Address>.Execute(() => service.GetByUserId(userId));
    }
}

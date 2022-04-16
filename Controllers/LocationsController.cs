using IEduZimAPI.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsRepository _locationsRepository;

        public LocationsController(ILocationsRepository locationsRepository)
        {
            _locationsRepository = locationsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _locationsRepository.GetAllAsync());

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var location = await _locationsRepository.GetByIdAsync(id);
            if (location.Data == null) return NotFound(location);

            return Ok(location);
        }

        [HttpGet("get-by-city-id/{cityId}")]
        public async Task<IActionResult> GetByCityId(int cityId)
        {
            var locations = await _locationsRepository.GetByCityIdAsync(cityId);
            if (locations.Data == null) return NotFound(locations);

            return Ok(locations);
        }
    }
}
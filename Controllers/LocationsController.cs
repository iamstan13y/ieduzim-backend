using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using IEduZimAPI.Models.Repository;
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

        [HttpGet("paged")]
        public Pagination<Paginator<Location>> GetPaged([FromQuery] PageRequest request) =>
            Pagination<Paginator<Location>>.FromObject(_locationsRepository.GetAllPagedAsync(request).Result);

        [HttpPost]
        public async Task<IActionResult> Add(LocationRequest request)
        {
            var result = await _locationsRepository.AddAsync(new Location
            {
                Area = request.Area,
                CityId = request.CityId,
                Distance = request.Distance
            });

            return Ok(result);
        }
    }
}
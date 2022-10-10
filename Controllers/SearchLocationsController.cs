using IEduZimAPI.Models.Local;
using IEduZimAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchLocationsController : ControllerBase
    {
        private readonly ISearchLocationsService _searchLocationsService;

        public SearchLocationsController(ISearchLocationsService searchLocationsService)
        {
            _searchLocationsService = searchLocationsService;
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> Get([FromQuery] LocationSearchRequest request, [FromQuery] Pagination pagination) => Ok(await _searchLocationsService.GetPagedLocationsAsync(request, pagination));
    }
}
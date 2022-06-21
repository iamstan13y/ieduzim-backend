using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using IEduZimAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HubsController : ControllerBase
    {
        private readonly IHubRepository _hubsRepository;

        public HubsController(IHubRepository hubsRepository)
        {
            _hubsRepository = hubsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _hubsRepository.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _hubsRepository.GetByIdAsync(id);
            if (!result.Succeeded) return NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HubRequest request)
        {
            var result = await _hubsRepository.AddAsync(new Hub
            {
                Name = request.Name,
                LocationId = request.LocationId
            });

            if (!result.Succeeded) return BadRequest(result);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateHubRequest request)
        {
            var result = await _hubsRepository.UpdateAsync(new Hub
            {
                Id = request.Id,
                LocationId= request.LocationId,
                Name = request.Name
            });

            if (!result.Succeeded) return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _hubsRepository.DeleteAsync(id));
    }
}
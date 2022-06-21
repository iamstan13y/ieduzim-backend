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
    }
}
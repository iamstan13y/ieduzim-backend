using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using IEduZimAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonLocationsController : ControllerBase
    {
        private readonly ILessonLocationRepository _lessonLocationRepository;

        public LessonLocationsController(ILessonLocationRepository lessonLocationRepository)
        {
            _lessonLocationRepository = lessonLocationRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _lessonLocationRepository.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Add(LessonLocationRequest request)
        {
            var result = await _lessonLocationRepository.AddAsync(new LessonLocation
            {
                Name = request.Name,
                TransportCosts = request.TransportCosts,
            });

            return Ok(result);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _lessonLocationRepository.GetByIdAsync(id);

            if (result == null) return NotFound(result);

            return Ok(result);
        }
    }
}
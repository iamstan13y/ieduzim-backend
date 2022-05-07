using IEduZimAPI.Models.Repository;
using Microsoft.AspNetCore.Http;
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
    }
}
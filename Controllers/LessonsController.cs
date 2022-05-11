using IEduZimAPI.Models.Local;
using IEduZimAPI.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonsController(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(LessonRequest request)
        {
            var result = await _lessonRepository.AddAsync(request);

            if (!result.Succeeded) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("get-by-studentId/{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await _lessonRepository.GetByStudentIdAsync(id));
    }
}
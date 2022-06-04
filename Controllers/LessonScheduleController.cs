using IEduZimAPI.Models.Local;
using IEduZimAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonScheduleController : ControllerBase
    {
        private readonly ILessonScheduleRepository _lessonScheduleRepository;

        public LessonScheduleController(ILessonScheduleRepository lessonScheduleRepository)
        {
            _lessonScheduleRepository = lessonScheduleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _lessonScheduleRepository.GetDaysAsync());
        
        [HttpGet("get-available")]
        public async Task<IActionResult> Get([FromQuery] AddressSearchRequest request)
        {
            var result = await _lessonScheduleRepository.GetByCriteriaAsync(request);

            if (!result.Succeeded) return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(LessonScheduleRequest request)
        {
            var result = _lessonScheduleRepository.Add(request);

            if (!result.Succeeded) return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("teacher-id/{teacherId}")]
        public async Task<IActionResult> GetByTeacherId(int teacherId) => Ok(await _lessonScheduleRepository.GetByTeacherIdAsync(teacherId));
    }
}
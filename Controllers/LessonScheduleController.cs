using IEduZimAPI.Models.Repository;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("teacher-id/{teacherId}")]
        public async Task<IActionResult> GetByTeacherId(int teacherId) => Ok(await _lessonScheduleRepository.GetByTeacherIdAsync(teacherId));
    }
}
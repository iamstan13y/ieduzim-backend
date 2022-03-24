using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TeacherActiveDaysController : BaseController<TeacherActiveDays, Models.Local.TeacherDays>
    {
        private new TeacherActiveDaysService service;
        public TeacherActiveDaysController() =>
            service = new TeacherActiveDaysService();

        [HttpGet]
        [Route("by-user-id/{userId}")]
        public Result<TeacherActiveDays> Get(string userId) =>
            ExecutionService<TeacherActiveDays>.Execute(() => service.GetByUserId(userId));
    }
}

using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class StudentsController : BaseController<Student, Models.Local.Students>
    {
        private new StudentService service;
        public StudentsController() =>
            service = new StudentService();

        [HttpGet]
        [Route("by-user-id/{userId}")]
        public Result<Student> Get(string userId) =>
            ExecutionService<Student>.Execute(() => service.GetByUserId(userId));
    }
}

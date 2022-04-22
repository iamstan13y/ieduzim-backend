using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TeachersController : BaseController<Teacher, Models.Local.Teachers>
    {
        private new TeacherService service;
        public TeachersController(UserManager<IdentityUser> userManager) =>
            service = new TeacherService(userManager);

        [HttpGet]
        [Route("by-user-id/{userId}")]
        public Result<Teacher> Get(string userId) =>
            ExecutionService<Teacher>.Execute(() => service.GetByUserId(userId));

        [HttpGet]
        [Route("default")]
        public Result<Teacher> GetDefault() =>
            ExecutionService<Teacher>.Execute(() => service.GetDefault());

        [HttpGet]
        [Route("verified")]
        public Pagination<Paginator<Teacher>> GetVerified([FromQuery] PageRequest request) =>
        PagedExecution<Paginator<Teacher>>.Execute(() => service.GetVerified(request));

        [HttpGet]
        [Route("subscribed-and-not-verified")]
        public Pagination<Paginator<Teacher>> GetSubscribedUnVerified([FromQuery] PageRequest request) =>
        PagedExecution<Paginator<Teacher>>.Execute(() => service.GetSubscribedUnverified(request));

        [HttpGet]
        [Route("unsubscribed")]
        public Pagination<Paginator<Teacher>> UnSubscribed([FromQuery] PageRequest request) =>
        PagedExecution<Paginator<Teacher>>.Execute(() => service.GetUnsubcribed(request));

        //[HttpPut]
        //[Route("unsubscribed/{userId}")]
        //public Result<Teacher> UnSubscribedUpdate(Teacher teacher,string userId) =>
        //ExecutionService<Teacher>.Execute(() => service.UpdateSubscriptionStatus(teacher, userId));
    }
} 

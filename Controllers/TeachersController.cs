using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using IEduZimAPI.Models.Repository;
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
        private readonly ITeacherRepository _teacherRepository;

        public TeachersController(UserManager<IdentityUser> userManager, ITeacherRepository teacherRepository)
        {
            service = new TeacherService(userManager);
            _teacherRepository = teacherRepository;
        }

        [HttpGet]
        [Route("by-user-id/{userId}")]
        public Result<Teacher> Get(string userId) =>
            ExecutionService<Teacher>.Execute(() => service.GetByUserId(userId));

        [HttpPost]
        public override Result<Teacher> Post(Teachers request)
        {
            var result = _teacherRepository.AddAsync(new Teacher
            {
                BankAccount = request.BankAccount,
                PhysicalAddress = request.PhysicalAddress,
                LocationId = request.LocationId,
                EducationalQualification = request.EducationalQualification,
                Gender = request.Gender,
                Name = request.Name,
                Occupation = request.Occupation,
                PhoneNumber = request.PhoneNumber,
                ProfilePictureUrl = request.ProfilePictureUrl,
                QualificationUrl = request.QualificationUrl,
                Surname = request.Surname,
                TitleId = request.TitleId,
                UserId = request.UserId
            }).Result;

            if (!result.Succeeded) return result;

            return result;
        }

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

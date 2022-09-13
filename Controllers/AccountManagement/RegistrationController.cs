using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Authorization;
using IEduZimAPI.Models;

namespace IEduZimAPI.Services.AccountServices
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        RegistrationService service;
        public RegistrationController(UserManager<IdentityUser> manager, RoleManager<IdentityRole> roleManager, AppDbContext context) =>
            service = new RegistrationService(manager, roleManager, context);

        [HttpPost]
        public Result<bool> Register([FromBody] Register register) =>
            ExecutionService.Execute(() => service.Register(register), "Account Creation Successful. Check your email for activation link.");

        [HttpPost]
        [Route("activate/student/{studentId}")]
        public Result<bool> ActivateStudent(int studentId) =>
            ExecutionService.Execute(() => service.ActivateStudent(studentId), "Account activated.");

        [HttpPost]
        [Authorize]
        [Route("activate/teacher")]
        public Result<bool> ActivateTeacher(string teacherId) =>
            ExecutionService.Execute(() => service.ActivateTeacher(teacherId), "Account activated.");
    }
}
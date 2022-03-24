using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;

namespace IEduZimAPI.Services.AccountServices
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        RegistrationService service;
        public RegistrationController(UserManager<IdentityUser> manager, RoleManager<IdentityRole> roleManager) =>
            service = new RegistrationService(manager, roleManager);

        [HttpPost]
        public Result<bool> Register([FromBody] Register register) =>
            ExecutionService.Execute(() => service.Register(register), "Account Creation Successful. Check your email for activation link.");

        [HttpPost]
        [Route("activate")]
        public Result<bool> ActivateAccount([FromBody] EmailConfirmation confirmation) =>
            ExecutionService.Execute(() => service.ActivateAccount(confirmation), "Account activated.");
    }
}
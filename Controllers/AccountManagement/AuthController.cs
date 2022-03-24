using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Services.AccountServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace IEduZimAPI.Controllers.AccountManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService service;
        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> manager) =>
            service = new AuthService(userManager, manager);

        [HttpPost]
        public Result<LoginResult> Login([FromBody] Login login) =>
            ExecutionService<LoginResult>.Execute(() => service.Login(login));

        [HttpPost]
        [Route("resetpassword")]
        public Result<bool> ResetPassword([FromBody] PasswordReset reset) =>
            ExecutionService.Execute(() => service.ResetPassword(reset), "Successfully recovered password. Proceed to Login");

        [HttpPost]
        [Route("logout")]
        public Result<bool> LogOut() =>
            ExecutionService.Execute(() => service.Logout());
    }
}
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

        [HttpPost("login/mobile")]
        public Result<LoginResult> Login([FromBody] Login login) =>
            ExecutionService<LoginResult>.Execute(() => service.Login(login));
        
        [HttpPost("login/admin")]
        public Result<LoginResult> LoginAdmin([FromBody] Login login) =>
            ExecutionService<LoginResult>.Execute(() => service.Login(login));
        
        [HttpPost("login/teacher")]
        public Result<LoginResult> LoginTeacher([FromBody] Login login) =>
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
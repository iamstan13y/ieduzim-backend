using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Services.AccountServices
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecoveryController : ControllerBase
    {
        private RecoveryService service;

        public RecoveryController(UserManager<IdentityUser> userManager) =>
             service = new RecoveryService(userManager);

        [HttpPost]
        [Route("forgot")]

        public Result<string> ForgotPassword([FromBody] ForgotPassword user) =>
            ExecutionService<string>.Execute(() => service.ResetPassword(user.Email), "Reset instructions have been emailed to your account.");

        [HttpPost]
        [Route("change")]
        public Result<bool> Changepassword([FromBody] Passwords passwords) =>
            ExecutionService.Execute(() => service.ChangePassword(passwords), "Password changed successfully.");
    }
}
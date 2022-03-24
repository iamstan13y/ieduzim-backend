using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Data.AccountManagement;
using IEduZimAPI.Service;
using IEduZimAPI.Services.EmailServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace IEduZimAPI.Services.AccountServices
{
    public class RegistrationService
    {
        private UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
        
        public RegistrationService(UserManager<IdentityUser> uManager, RoleManager<IdentityRole> rManager)
        {
            userManager = uManager;
            roleManager = rManager;
        }

        public void Register(Register register)
        {
            string password = register.Phonenumber.Substring(register.Phonenumber.Length - 6);
            IdentityUser user = new IdentityUser() { UserName = register.Email, Email = register.Email, PhoneNumber = register.Phonenumber, EmailConfirmed = true};
            userManager.CreateAsync(user, password).Result.Validate();
            userManager.AddToRoleAsync(user, register.Role).Result.Validate();
            SendConfirmationEmail(GenerateConfirmationCode(userManager.FindByEmailAsync(user.Email).Result.Validate().Id), user, register.Role);
        }

        private string GenerateConfirmationCode(string userId)
        {
            Random generator = new Random();
            string code = generator.Next(0, 1000000).ToString("D6");
            new VerificationService(userManager).Add(new AspNetVerificationCode() { Code = code, Expiry = DateTime.Now.AddHours(2), UserId = userId, ConfirmationFailedCount = 0 }, string.Empty);
            return code;
        }

        private void SendConfirmationEmail(string token, IdentityUser user, string role)
        {
            var body = new Dictionary<string, string>
            {
                {"{role}", role },
                {"{emailAddress}", user.Email},
                {"{token}", token },
                {"{subject}", "Account Verification Code"}
            };
            EmailService.Send(Models.Enums.EmailType.ConfirmAccount, body);
        }

        public void ActivateAccount(EmailConfirmation confirmation)
        {
            var user = userManager.FindByNameAsync(confirmation.Username).Result.ValidateUser(confirmation.Username);
            new VerificationService(userManager).CheckVerification(user, confirmation);
        }
    }
}

using System;
using System.Collections.Generic;
using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Data.AccountManagement;
using IEduZimAPI.Service;
using IEduZimAPI.Services.EmailServices;
using Microsoft.AspNetCore.Identity;

namespace IEduZimAPI.Services.AccountServices
{
    public class RecoveryService
    {
        private UserManager<IdentityUser> userManager;
        public RecoveryService(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public string ResetPassword(string email)
        {
            var user = userManager.FindByEmailAsync(email).Result.ValidateEmail(email);
            var token = GenerateConfirmationCode(user.Id);
            new VerificationService(userManager).SetTempPassword(user, token);
            SendResetEmail(email, token, user);
            return token;
        }

        private string GenerateConfirmationCode(string userId)
        {
            Random generator = new Random();
            string code = generator.Next(0, 1000000).ToString("D6");
            new VerificationService(userManager).Add(new AspNetVerificationCode() { Code = code, Expiry = DateTime.Now.AddHours(2), UserId = userId, ConfirmationFailedCount = 0 }, string.Empty);
            return code;
        }

        private void SendResetEmail(string userEmail, string token, IdentityUser user)
        {
            var dictionary = new Dictionary<string, string>
                {
                    {"{username}", user.UserName},
                    {"{token}", $"{token}"},
                    {"{emailAddress}", userEmail },
                    {"{subject}", "Password Reset" }
                };
            EmailService.Send(Models.Enums.EmailType.ForgotPassword, dictionary);
        }

        public void ChangePassword(Passwords passwords)
        {
            var user = userManager.FindByIdAsync(passwords.UserId).Result;
            userManager.ChangePasswordAsync(user, passwords.CurrentPassword, passwords.NewPassword).Result.Validate();
        }
    }
}
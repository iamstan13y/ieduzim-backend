using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Data.AccountManagement;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Web.Helpers;

namespace IEduZimAPI.Services.EmailServices
{
    public class VerificationService:BaseService<AspNetVerificationCode>
    {
        private UserManager<IdentityUser> manager;

        public VerificationService(UserManager<IdentityUser> userManager)
        {
            manager = userManager;
        }
        public void CheckVerification(IdentityUser user, EmailConfirmation confirmation)
        {
            var u = context.AspNetVerificationCodes.FirstOrDefault(a => a.UserId == user.Id);
            if (u == null) throw new Exception("Account not found. Please try registering again.");
            if (u.Expiry < DateTime.Now)
            {
                RemoveVerificationCode(u);
                RemoveUserFromRegistrations(user);
                throw new Exception("Verification Code Expired. Please register again and verify your account within two hours.");
            }
            if (u.Code != confirmation.Token)
            {
                u.ConfirmationFailedCount = u.ConfirmationFailedCount + 1;
                context.SaveChanges();
                if(u.ConfirmationFailedCount <= 2)
                    throw new Exception("Confirmation code mismatch");
                if(u.ConfirmationFailedCount == 3)
                {
                    RemoveVerificationCode(u);
                    RemoveUserFromRegistrations(user);
                    throw new Exception("Confirmation code mismatch. Please register again because this code is no longer valid.");
                }
            }
            else
            {
                user.EmailConfirmed = true;
                manager.UpdateAsync(user).Result.Validate();
                RemoveVerificationCode(u);
            }
        }

        private void RemoveVerificationCode(AspNetVerificationCode code)
        {
            context.AspNetVerificationCodes.Remove(code);
            context.SaveChanges();
        }

        private void RemoveUserFromRegistrations(IdentityUser identityUser)
        {
            var roles = manager.GetRolesAsync(identityUser).Result;
            manager.RemoveFromRolesAsync(identityUser, roles).Wait();
            manager.DeleteAsync(identityUser).Wait();
        }

        public void SetTempPassword(IdentityUser user, string password)
        {
            var exists = context.AspNetTempPasswords.Find(user.Id);
            if (exists != null) throw new Exception("Password reset instructions already sent to your email.");
            context.AspNetTempPasswords.Add(new AspNetTempPassword() { UserId = user.Id, Password = Crypto.HashPassword(password), Expiry = DateTime.Now.AddHours(2)}) ;
            context.SaveChanges();
        }

        public AspNetTempPassword GetTempPassword(IdentityUser user) => context.AspNetTempPasswords.Find(user.Id);

        public void DeleteTempPassword(AspNetTempPassword tempPassword)
        {
            context.Remove(tempPassword);
            context.SaveChanges();
        }
    }
}

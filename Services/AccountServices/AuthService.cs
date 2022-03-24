﻿using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Services.EmailServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Web.Helpers;

namespace IEduZimAPI.Services.AccountServices
{
    public class AuthService
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        
        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public LoginResult Login(Login login)
        {
            var user = (IdentityUser)userManager.FindByNameAsync(login.Username).Result.ValidateUser(login.Username);
            var tempPassword = new VerificationService(userManager).GetTempPassword(user);
            if (!userManager.IsEmailConfirmedAsync(user).Result)
                throw new Exception($"Your account is not active");
            if (!userManager.CheckPasswordAsync(user, login.Password).Result)
            {
                var t = Crypto.VerifyHashedPassword(user.PasswordHash, login.Password);
                if (tempPassword == null || !Crypto.VerifyHashedPassword(tempPassword.Password, login.Password))
                    throw new Exception($"Incorrect Password");             
                if (tempPassword.Expiry < DateTime.Now)
                {
                    new VerificationService(userManager).DeleteTempPassword(tempPassword);
                    throw new Exception("Temporary Password Expired. Request Reset and Try Again.");
                }
                if (Crypto.VerifyHashedPassword(tempPassword.Password, login.Password))
                {
                    user.PasswordHash = tempPassword.Password;
                    userManager.UpdateAsync(user);
                    new VerificationService(userManager).DeleteTempPassword(tempPassword);
                    return new LoginResult(new TokenService(userManager).TokenBuilder(user), user);
                }
            }
            else if (tempPassword != null) new VerificationService(userManager).DeleteTempPassword(tempPassword);
            return new LoginResult(new TokenService(userManager).TokenBuilder(user), user);
        }

        public void ResetPassword(PasswordReset reset)
        {
            var user = userManager.FindByEmailAsync(reset.Email).Result.ValidateEmail(reset.Email);
            userManager.ResetPasswordAsync(user, reset.Token.Replace(" ", "+"), reset.Password).Result.Validate();
        }

        public void Logout() => signInManager.SignOutAsync();
    }
}
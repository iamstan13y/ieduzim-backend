﻿using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Data.AccountManagement;
using IEduZimAPI.Service;
using IEduZimAPI.Services.EmailServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace IEduZimAPI.Services.AccountServices
{
    public class RegistrationService
    {
        private UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext _context;

        public RegistrationService(UserManager<IdentityUser> uManager, RoleManager<IdentityRole> rManager, AppDbContext context)
        {
            userManager = uManager;
            roleManager = rManager;
            _context = context;
        }

        public Result<IdentityUser> Register(Register register)
        {
            try
            {
                string password = register.Phonenumber[^6..];
                IdentityUser user = new() { UserName = register.Email, Email = register.Email, PhoneNumber = register.Phonenumber, EmailConfirmed = true };
                userManager.CreateAsync(user, password).Result.Validate();
                userManager.AddToRoleAsync(user, register.Role).Result.Validate();
                SendConfirmationEmail(GenerateConfirmationCode(userManager.FindByEmailAsync(user.Email).Result.Validate().Id), user, register.Role);

                return new Result<IdentityUser>(user);
            }
            catch (Exception)
            {
                return new Result<IdentityUser>(false, "Failed to complete registration.", null);
            }

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

        public void ActivateTeacher(int teacherId)
        {
            var account = _context.Teachers.Find(teacherId);
            if (account == null) throw new Exception("Account not found");
            account.IsActive = true;

            _context.Teachers.Update(account);
            _context.SaveChanges();
        }
    }
}
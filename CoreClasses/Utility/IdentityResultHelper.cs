using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace IEduZimAPI.CoreClasses
{
    public static class IdentityResultHelper
    {
        public static void Validate(this IdentityResult result)
        {
            if (result == null)
                throw new Exception("Invalid result");
            if (!result.Succeeded)
                throw new Exception(string.Join(',', result.Errors.Select(error => error.Description)));
        }


        public static IdentityUser ValidateEmail(this IdentityUser user, string email) =>
            user ?? throw new Exception($"Email not found");

        public static IdentityUser ValidateUser(this IdentityUser user, string username) =>
            user ?? throw new Exception($"User not found");

        public static IdentityUser ValidateUserId
            (this IdentityUser user) =>
            user ?? throw new Exception($"User could not be found");

        public static IdentityUser Validate(this IdentityUser user) =>
            user ?? throw new Exception("User not authenticated.");

    }
}
using Microsoft.AspNetCore.Identity;
using System;
using System.Text.RegularExpressions;

namespace IEduZimAPI.CoreClasses
{
    public class IdentityInitializer
    {
        public static void SeedIdentityData(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            SeedRoles(roleManager);
            SeedUser(userManager, roleManager);
        }
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetValues(typeof(Models.Enums.UserRole)))
                if (!roleManager.RoleExistsAsync(Regex.Replace(role.ToString(), "(\\B[A-Z])", " $1")).Result)
                {
                    IdentityRole userRole = new IdentityRole() { Name = Regex.Replace(role.ToString(), "(\\B[A-Z])", " $1") };
                    IdentityResult roleResult = roleManager.CreateAsync(userRole).Result;
                }
        }

        public static void SeedUser(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                IdentityUser user = new IdentityUser() { UserName = "admin", Email = Startup.configuration["EmailCredentials:Username"], PhoneNumber = Startup.configuration["EmailCredentials:PhoneNumber"], EmailConfirmed = true, PhoneNumberConfirmed = true };
                userManager.CreateAsync(user, "Password@123").Result.Validate();
                var role = roleManager.FindByNameAsync(nameof(Models.Enums.UserRole.Administrator)).Result;
                userManager.AddToRoleAsync(user, role.Name).Result.Validate();
            }
        }
    }
}

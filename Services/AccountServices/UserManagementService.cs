using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Local;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace IEduZimAPI.Services.AccountServices
{
    public class UserManagementService: BaseService<IdentityUser>
    {
        private UserManager<IdentityUser> manager;
        private RoleManager<IdentityRole> rManager;

        public UserManagementService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            manager = userManager;
            rManager = roleManager;
        }
            
        public IEnumerable<IdentityUser> GetByRole(string roleId)
        {
            var role = rManager.FindByIdAsync(roleId).Result;
            return manager.GetUsersInRoleAsync(role.Name).Result;
        }

        public IdentityUser UpdateUserDetails(User user)
        {
            var usr = manager.FindByIdAsync(user.Id).Result;
            if (!string.IsNullOrEmpty(user.Email))
            {
                usr.Email = user.Email;
                usr.NormalizedEmail = user.Email.ToUpper();
            }
            if (!string.IsNullOrEmpty(user.PhoneNumber))
                usr.PhoneNumber = user.PhoneNumber;
            var u = manager.UpdateAsync(usr).Result;
            return usr;
        }


    }
}

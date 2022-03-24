using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Local;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IEduZimAPI.Services.AccountServices
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsersController : ControllerBase
    {
        UserManagementService service;
        public UsersController(UserManager<IdentityUser> manager, RoleManager<IdentityRole> roleManager) =>
            service = new UserManagementService(manager, roleManager);

        [HttpGet]
        public Result<IEnumerable<IdentityUser>> GetUsers() =>
            ExecutionService<IEnumerable<IdentityUser>>.Execute(() => service.Get());

        [HttpGet]
        [Route("{id}")]
        public Result<IdentityUser> GetUser(string id) =>
            ExecutionService<IdentityUser>.Execute(() => service.Get(id));

        [HttpGet]
        [Route("by-role")]
        public Result<IEnumerable<IdentityUser>> GetByRole([FromQuery] string roleId) =>
            ExecutionService<IEnumerable<IdentityUser>>.Execute(() => service.GetByRole(roleId));

        [HttpPut]
        public Result<IdentityUser> UpdateUserDetails([FromBody] User user) =>
            ExecutionService<IdentityUser>.Execute(() => service.UpdateUserDetails(user)); 
    }
}

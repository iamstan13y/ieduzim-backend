﻿using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Services.AccountServices
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        readonly RegistrationService service;
        public RegistrationController(UserManager<IdentityUser> manager, RoleManager<IdentityRole> roleManager, AppDbContext context) =>
            service = new RegistrationService(manager, roleManager, context);

        [HttpPost]
        public Result<IdentityUser> Register([FromBody] Register register) =>
            service.Register(register);

        [HttpPost]
        [Route("activate/teacher/{teacherId}")]
        public Result<bool> ActivateTeacher(int teacherId) =>
            ExecutionService.Execute(() => service.ActivateTeacher(teacherId), "Account activated.");
    }
}
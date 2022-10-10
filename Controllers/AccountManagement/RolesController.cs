using IEduZimAPI.CoreClasses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IEduZimAPI.Services.AccountServices
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private BaseService<IdentityRole> service;
        public RolesController() => service = new BaseService<IdentityRole>();

        [HttpGet]
        public Result<IEnumerable<IdentityRole>> Get() =>
            ExecutionService<IEnumerable<IdentityRole>>.Execute(() => service.Get());

    }
}

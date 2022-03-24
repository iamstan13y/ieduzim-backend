using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ExamTypesController : BaseController<ExamTypes, Models.Local.BaseName>
    {
        public ExamTypesController() : base(new BaseService<ExamTypes>()) { }
    }
}

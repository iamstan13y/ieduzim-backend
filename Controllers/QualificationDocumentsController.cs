using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace IEduZimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class QualificationDocumentsController : BaseController<QualificationDocuments, Models.Local.QualificationDocument>
    {
        public QualificationDocumentsController() : base(new BaseService<QualificationDocuments>()) { }
    }
}
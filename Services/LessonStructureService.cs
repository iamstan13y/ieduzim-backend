using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IEduZimAPI.Services
{
    public class LessonStructureService : BaseService<LessonStructure> { 
    
        public Paginator<LessonStructure> GetPagedByUserId(PageRequest request, string userId)
        {
            if (request == null) request = new PageRequest() { PageNumber = 1, PageSize = 10 };
            var req = context.LessonStructures.Include(i=> i.Subject).Include(c=> c.Level).Where(a => a.UserId.Equals(userId));
            if (request.Active == true)
                req = req.Where(a => a.Active == true);
            if (request.SortParam != null)
                req = Sort(req, request);
            var total = req.CountAsync().Result;
            req = req.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
            return new Paginator<LessonStructure>(request, total, req);
        }
    }   
}

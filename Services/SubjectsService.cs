using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IEduZimAPI.Services
{
    public class SubjectsService : BaseService<Subject>
    {
        public Paginator<Subject> GetPagedByLevel(PageRequest request, int levelId)
        {
            if (request == null) request = new PageRequest() { PageNumber = 1, PageSize = 10 };
            var req = context.Subjects.Include(b => b.Currency).Where(a => a.LevelId == levelId);
            if (request.Active == true)
                req = req.Where(a => a.Active == true);
            if (request.SortParam != null)
                req = Sort(req, request);
            var total = req.CountAsync().Result;
            req = req.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
            return new Paginator<Subject>(request, total, req);
        }
    }
}

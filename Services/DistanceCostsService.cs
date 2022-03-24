using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IEduZimAPI.Services
{
    public class DistanceCostsService : BaseService<DistancePrices>
    {
        public override Paginator<DistancePrices> GetPaged(PageRequest request)
        {
            if (request == null) request = new PageRequest() { PageNumber = 1, PageSize = 10 };
            var req = context.DistancePrices.Include(b => b.Currency).AsQueryable();
            if (request.SortParam != null)
                req = Sort(req, request);
            var total = req.CountAsync().Result;
            req = req.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
            return new Paginator<DistancePrices>(request, total, req);
        }
    }
}

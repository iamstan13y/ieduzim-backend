using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IEduZimAPI.Services
{
    public class RolePaymentPeriodsService : BaseService<RolePaymentsSettings>
    {
        public override Paginator<RolePaymentsSettings> GetPaged(PageRequest request)
        {
            if (request == null) request = new PageRequest() { PageNumber = 1, PageSize = 10 };
            var req = context.RolePaymentSettings.Include(b => b.Role).Include(c=> c.PaymentPeriod).AsQueryable();
            if (request.SortParam != null)
                req = Sort(req, request);
            var total = req.CountAsync().Result;
            req = req.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
            return new Paginator<RolePaymentsSettings>(request, total, req);
        }

        public override IEnumerable<RolePaymentsSettings> Get()=>
            context.RolePaymentSettings.Include(b => b.Role).Include(c => c.PaymentPeriod);
    
    }
}
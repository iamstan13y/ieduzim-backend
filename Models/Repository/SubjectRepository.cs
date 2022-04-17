using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly IEduContext _context;
        private readonly AppDbContext _appDbContext;

        public SubjectRepository(IEduContext context, AppDbContext appDbContext)
        {
            _context = context;
            _appDbContext = appDbContext;
        }

        public async Task<Result<IEnumerable<Subject>>> GetAllSubjectsAsync()
        {
            var subjects = await _context.Subjects.Include(x => x.Level).ToListAsync();
            subjects.ForEach(x => x.ZwlPrice = CalculateZwlPrice(x.Price));

            return new Result<IEnumerable<Subject>>(subjects);
        }

        public Task<Paginator<Subject>> GetAllSubjectsPagedAsync(PageRequest request)
        {
            if (request == null) request = new PageRequest() { PageNumber = 1, PageSize = 10 };
            var req = _context.Set<Subject>().AsQueryable<Subject>();
            if (request.SortParam != null)
                req = Sort(req, request);
            var total = req.CountAsync().Result;
            req.ForEachAsync(x => x.ZwlPrice = CalculateZwlPrice(x.Price));
            req.Include(x => x.Level).AsQueryable();
            req = req.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
            return Task.FromResult(new Paginator<Subject>(request, total, req));
        }

        public IQueryable<Subject> Sort(IQueryable<Subject> req, PageRequest request)
        {
            var param = Expression.Parameter(typeof(Subject), "item");
            var sortExpression = Expression.Lambda<Func<Subject, object>>
                    (Expression.Convert(Expression.Property(param, request.SortParam), typeof(object)), param);
            if (request.Desc)
                req = req.OrderByDescending(sortExpression);
            else req = req.OrderBy(sortExpression);
            return req;
        }

        private double CalculateZwlPrice(string UsdPrice)
        {
            var rate = _appDbContext.ExchangeRates.Where(x => x.CurrencyId == 2).FirstOrDefault().Rate;

            return Math.Round(Convert.ToDouble(UsdPrice) * rate, 2);
        }
    }
}
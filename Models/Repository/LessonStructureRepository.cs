using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class LessonStructureRepository : ILessonStructureRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IEduContext _context;

        public LessonStructureRepository(AppDbContext appDbContext, IEduContext context)
        {
            _appDbContext = appDbContext;
            _context = context;
        }

        public Task<Paginator<LessonStructure>> GetPagedByUserId(PageRequest request, string userId)
        {
            if (request == null) request = new PageRequest() { PageNumber = 1, PageSize = 10 };
            var req = _context.LessonStructures.Include(i => i.Subject).Include(c => c.Level).Include(x => x.Teacher).Where(a => a.TeacherId.Equals(userId)).ToList();
            req.ForEach(a => a.Subject.ZwlPrice = CalculateZwlPrice(a.Subject.Price));
            if (request.Active == true)
                req = req.Where(a => a.Active == true).ToList();
            if (request.SortParam != null)
                req = Sort((IQueryable<LessonStructure>)req, request).ToList();
            var total = req.Count;
            req = req.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
            return Task.FromResult(new Paginator<LessonStructure>(request, total, req));
        }

        public IQueryable<LessonStructure> Sort(IQueryable<LessonStructure> req, PageRequest request)
        {
            var param = Expression.Parameter(typeof(LessonStructure), "item");
            var sortExpression = Expression.Lambda<Func<LessonStructure, object>>
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
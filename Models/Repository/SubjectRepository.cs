using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private double CalculateZwlPrice(string UsdPrice)
        {
            var rate = _appDbContext.ExchangeRates.Where(x => x.CurrencyId == 2).FirstOrDefault().Rate;

            return Math.Round(Convert.ToDouble(UsdPrice) * rate, 2);
        }
    }
}
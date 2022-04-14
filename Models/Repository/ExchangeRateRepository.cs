using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly AppDbContext _context;

        public ExchangeRateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<ExchangeRate>> AddAsync(ExchangeRate exchangeRate)
        {
            await _context.ExchangeRates.AddAsync(exchangeRate);
            await _context.SaveChangesAsync();

            return new Result<ExchangeRate>(exchangeRate);
        }

        public async Task<Result<IEnumerable<ExchangeRate>>> GetAllAsync()
        {
            var rates = await _context.ExchangeRates.Include(x => x.Currencies).ToListAsync();
            return new Result<IEnumerable<ExchangeRate>>(rates);
        }

        public Task<Result<ExchangeRate>> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<ExchangeRate>> UpdateAsync(ExchangeRate exchangeRate)
        {
            throw new System.NotImplementedException();
        }
    }
}
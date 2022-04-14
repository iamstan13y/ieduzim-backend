using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        public Task<Result<ExchangeRate>> AddAsync(ExchangeRate exchangeRate)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<IEnumerable<ExchangeRate>>> GetAllAsync()
        {
            throw new System.NotImplementedException();
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
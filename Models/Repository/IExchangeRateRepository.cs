using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface IExchangeRateRepository
    {
        Task<Result<IEnumerable<ExchangeRate>>> GetAllAsync();
        Task<Result<ExchangeRate>> GetByIdAsync(int id);
        Task<Result<ExchangeRate>> AddAsync(ExchangeRate exchangeRate);
        Task<Result<ExchangeRate>> UpdateAsync(ExchangeRate exchangeRate);
    }
}
using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface IHubRepository
    {
        Task<Result<IEnumerable<Hub>>> GetAllAsync();
        Task<Result<Hub>> AddAsync(Hub hub);
        Task<Result<Hub>> GetByIdAsync(int id);
        Task<Result<Hub>> UpdateAsync(Hub hub);
        Task<Result<Hub>> DeleteAsync(int id);
    }
}
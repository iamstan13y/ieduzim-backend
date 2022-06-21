using IEduZimAPI.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface IHubRepository
    {
        Task<IEnumerable<Hub>> GetAllAsync();
        Task<Hub> AddAsync(Hub hub);
        Task<Hub> GetByIdAsync(int id);
        Task<Hub> UpdateAsync(Hub hub);
        Task<Hub> DeleteAsync(int id);
    }
}
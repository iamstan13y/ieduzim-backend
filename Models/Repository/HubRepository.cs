using IEduZimAPI.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class HubRepository : IHubRepository
    {
        public Task<Hub> AddAsync(Hub hub)
        {
            throw new System.NotImplementedException();
        }

        public Task<Hub> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Hub>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Hub> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Hub> UpdateAsync(Hub hub)
        {
            throw new System.NotImplementedException();
        }
    }
}
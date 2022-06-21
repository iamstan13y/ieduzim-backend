using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class HubRepository : IHubRepository
    {
        private readonly AppDbContext _context;

        public HubRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<Result<Hub>> AddAsync(Hub hub)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<Hub>> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<IEnumerable<Hub>>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<Hub>> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<Hub>> UpdateAsync(Hub hub)
        {
            throw new System.NotImplementedException();
        }
    }
}
using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System;
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

        public async Task<Result<Hub>> AddAsync(Hub hub)
        {
            try
            {
                await _context.Hubs.AddAsync(hub);
                await _context.SaveChangesAsync();

                return new Result<Hub>(hub);
            }
            catch (Exception)
            {
                return new Result<Hub>(false, "Failed to save", null);
            }
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
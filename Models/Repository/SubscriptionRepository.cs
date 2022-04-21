using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AppDbContext _context;

        public SubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Subscription>> AddAsync(Subscription subscription)
        {
            try
            {
                await _context.Subscriptions.AddAsync(subscription);
                await _context.SaveChangesAsync();

                return new Result<Subscription>(subscription);
            }
            catch (Exception)
            {
                return new Result<Subscription>(false, "Failed to create!", null);
            }
        }

        public async Task<Result<IEnumerable<Subscription>>> GetAllAsync()
        {
            var subscriptions = await _context.Subscriptions.ToListAsync();
            return new Result<IEnumerable<Subscription>>(subscriptions);
        }

        public async Task<Result<Subscription>> GetByIdAsync(int id)
        {
            var subscription = await _context.Subscriptions.FirstOrDefaultAsync(x => x.Id == id);
            return new Result<Subscription>(subscription);
        }

        public Task<Result<IEnumerable<Subscription>>> GetByStudentIdAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<Subscription>>> GetByTeacherIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
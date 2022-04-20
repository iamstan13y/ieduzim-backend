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
;    }
}
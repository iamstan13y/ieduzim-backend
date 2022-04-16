using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class LocationsRepository : ILocationsRepository
    {
        private readonly AppDbContext _context;

        public LocationsRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<Result<Location>> AddAsync(Location location)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Result<IEnumerable<Location>>> GetAllAsync()
        {
            var locations = await _context.Locations.ToListAsync();
            return new Result<IEnumerable<Location>>(locations);
        }

        public Task<Result<IEnumerable<Location>>> GetByCityIdAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Result<Location>> GetByIdAsync(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            return new Result<Location>(location);
        }

        public Task<Result<Location>> UpdateAsync(Location location)
        {
            throw new System.NotImplementedException();
        }
    }
}
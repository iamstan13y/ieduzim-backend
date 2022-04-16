using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Result<Location>> AddAsync(Location location)
        {
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();

            return new Result<Location>(location);
        }

        public async Task<Result<IEnumerable<Location>>> GetAllAsync()
        {
            var locations = await _context.Locations.ToListAsync();
            return new Result<IEnumerable<Location>>(locations);
        }

        public async Task<Result<IEnumerable<Location>>> GetByCityIdAsync(int cityId)
        {
            var locations = await _context.Locations.Where(x => x.CityId == cityId).ToListAsync();
            return new Result<IEnumerable<Location>>(locations);
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
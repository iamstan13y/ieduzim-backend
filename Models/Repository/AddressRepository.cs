using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;

        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<LocalAddress>>> GetByCriteriaAsync(AddressSearchRequest request)
        {
            var student = await _context.Students.Where(x => x.UserId == request.UserId).FirstOrDefaultAsync();
            if (student == null) return new Result<IEnumerable<LocalAddress>>(false, "Student not found", null);

            var studentLocation = await _context.Locations.Where(x => x.Id == student.LocationId).FirstOrDefaultAsync();

            var addresses = await _context.Addresses
                .Where(x => x.Location.CityId == studentLocation.CityId && x.Location.Distance <= 10)
                .OrderBy(x => x.Location.Distance)
                .Include(x => x.Location)
                .ToListAsync();

            return new Result<IEnumerable<LocalAddress>>(addresses);
        }
    }
}
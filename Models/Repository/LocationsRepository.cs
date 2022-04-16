using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class LocationsRepository : ILocationsRepository
    {
        public Task<Result<Location>> AddAsync(Location location)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<IEnumerable<Location>>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<IEnumerable<Location>>> GetByCityIdAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<Location>> GetByIdAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<Location>> UpdateAsync(Location location)
        {
            throw new System.NotImplementedException();
        }
    }
}
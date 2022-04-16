using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface ILocationsRepository
    {
        Task<Result<Location>> AddAsync(Location location);
        Task<Result<Location>> UpdateAsync(Location location);
        Task<Result<IEnumerable<Location>>> GetAllAsync();
        Task<Result<IEnumerable<Location>>> GetByCityIdAsync(int cityId);
        Task<Result<Location>> GetByIdAsync(int id);
    }
}
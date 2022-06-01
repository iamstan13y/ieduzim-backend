using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface IAddressRepository
    {
        Task<Result<IEnumerable<LocalAddress>>> GetByCriteriaAsync(AddressSearchRequest request);
    }
}
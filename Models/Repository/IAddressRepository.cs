using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public interface IAddressRepository
    {
        Task<Result<IEnumerable<Address>>> GetByCriteriaAsync();
    }
}
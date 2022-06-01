using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEduZimAPI.Models.Repository
{
    public class AddressRepository : IAddressRepository
    {
        public Task<Result<IEnumerable<Address>>> GetByCriteriaAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
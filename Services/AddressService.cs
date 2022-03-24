using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IEduZimAPI.Services
{
    public class AddressService : BaseService<Address>
    {
        public Address GetByUserId(string userId) =>
           context.Addresses.Include(a=> a.City).Include(b=> b.Province).FirstOrDefault(a => a.UserId == userId);
    }
}

using IEduZimAPI.CoreClasses;
using IEduZimAPI.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace IEduZimAPI.Services
{
    public class BankDetailsService : BaseService<BankDetails>
    {
        public IEnumerable<BankDetails> GetByUserId(string userId) =>
          context.BankDetails.Include(i => i.User).Include(u => u.AccountType).Where(a => a.UserId == userId);
    }
}

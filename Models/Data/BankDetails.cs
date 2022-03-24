using Microsoft.AspNetCore.Identity;

namespace IEduZimAPI.Models.Data
{
    public class BankDetails:Local.BankDetail
    {
        public int Id { get; set; }
        public virtual IdentityUser User { get; set; }
        public virtual Banks AccountType { get; set; }
    }
}

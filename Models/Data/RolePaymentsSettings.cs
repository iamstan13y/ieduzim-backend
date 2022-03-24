using IEduZimAPI.Models.Local;
using Microsoft.AspNetCore.Identity;

namespace IEduZimAPI.Models.Data
{
    public class RolePaymentsSettings:RolePaymentSetting
    {
        public int Id { get; set; }
        public virtual PaymentPeriods PaymentPeriod { get; set; }
        public virtual IdentityRole Role { get; set; }
    }
}

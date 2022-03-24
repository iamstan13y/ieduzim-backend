using System;

namespace IEduZimAPI.Models.Data.AccountManagement
{
    public class AspNetVerificationCode
    {
        public string UserId { get; set; }
        public string Code { get; set; }
        public DateTime Expiry { get; set; }
        public int ConfirmationFailedCount { get; set; }
    }
}

using System;

namespace IEduZimAPI.Models.Data.AccountManagement
{
    public class AspNetTempPassword
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public DateTime Expiry { get; set; }
    }
}

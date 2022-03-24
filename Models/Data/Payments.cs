using Microsoft.AspNetCore.Identity;
using System;

namespace IEduZimAPI.Models.Data
{
    public class Payments:Local.Payment
    {
        public int Id { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string ModifiedBy { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}

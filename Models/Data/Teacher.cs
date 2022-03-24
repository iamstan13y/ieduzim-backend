using Microsoft.AspNetCore.Identity;

namespace IEduZimAPI.Models.Data
{
    public class Teacher :  Local.Teachers
    {
        public int Id { get; set; }
        public virtual IdentityUser User { get; set; }
        public virtual Title Title{ get; set;}
    }
}

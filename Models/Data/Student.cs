using Microsoft.AspNetCore.Identity;

namespace IEduZimAPI.Models.Data
{
    public class Student: Local.Students
    {
        public int Id { get; set; }
        public virtual IdentityUser User { get; set; }
        public virtual Title Title { get; set; }

    }
}

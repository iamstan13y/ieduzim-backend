using IEduZimAPI.Models.Data;
using IEduZimAPI.Models.Local;
using Microsoft.AspNetCore.Identity;

namespace IEduZimAPI.Models
{
    public class TeacherDocuments : TeacherDocument
    {
        public int Id { get; set; }
        public virtual QualificationDocuments QualificationDocuments { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}

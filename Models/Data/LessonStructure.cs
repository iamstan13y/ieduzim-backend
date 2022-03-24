using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace IEduZimAPI.Models.Data
{
    public class LessonStructure:Local.LocalLessonStructure
    {
        public int Id { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual Level Level { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}

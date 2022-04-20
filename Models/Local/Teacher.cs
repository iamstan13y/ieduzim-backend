using System.ComponentModel.DataAnnotations.Schema;

namespace IEduZimAPI.Models.Local
{
    public class Teachers
    {
        [NotMapped]
        public string PhoneNumber { get; set; }
        public int TitleId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string QualificationDescription { get; set; }
        public string ProfilePicture { get; set; }
        public string UserId { get; set; }
        public bool Verified { get; set; }
        public bool Subscribed { get; set; }
    }
}

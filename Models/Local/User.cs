using System.ComponentModel.DataAnnotations;

namespace IEduZimAPI.Models.Local
{
    public class User
    {
        [Required]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}

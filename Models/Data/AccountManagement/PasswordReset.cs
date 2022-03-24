namespace IEduZimAPI.Models.Data
{
    public class PasswordReset
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}

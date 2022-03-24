namespace IEduZimAPI.Models.Data
{
    public class EmailConfirmation
    {
        public EmailConfirmation() { }
        public EmailConfirmation(string username, string token)
        {
            Token = token;
            Username = username;
        }

        public string Token { get; set; }
        public string Username { get; set; }
    }
}

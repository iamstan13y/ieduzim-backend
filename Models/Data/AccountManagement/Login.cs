namespace IEduZimAPI.Models.Data
{
    public class Login
    {
        public string Username { set; get; }
        public string Password { get; set; }
    }

    public class LoginResult
    {
        public string Token { set; get; }
        public object UserAccount { get; set; }

        public LoginResult(string token, object userAccount)
        {
            Token = token;
            UserAccount = userAccount;
        }
    }
}
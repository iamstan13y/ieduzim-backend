using Microsoft.AspNetCore.Identity;

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
        public IdentityUser User { get; set; }

        public LoginResult(string token, IdentityUser user)
        {
            Token = token;
            User = user;
        }
    }
}
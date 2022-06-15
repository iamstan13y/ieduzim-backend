using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using IEduZimAPI.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace IEduZimAPI.Services.AccountServices
{
    public class TokenService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly StudentService _studentService;

        public TokenService(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            _studentService = new StudentService();
        }

        public static string GetName(string token, UserManager<IdentityUser> userManager)
        {
            if (string.IsNullOrEmpty(token)) return null;
            string userId = new JwtSecurityToken(token)?.Claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
            return userManager.FindByIdAsync(userId).Result?.UserName;
        }

        public string TokenBuilder(IdentityUser user)
        {
            var roles = userManager.GetRolesAsync(user).Result;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.configuration["Jwt:Key"]));

            Student student = new() { Id = default};
            if (roles[0] == "Student")
            {
                student = _studentService.GetByUserId(user.Id);
            }
            var claims = new Claim[] {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("Role", roles.Count == 0 ? "" : roles[0]),
                    new Claim("UserId", user.Id),
                    new Claim("Email", user.Email),
                    new Claim("StudentId", student.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

            var jwtToken = new JwtSecurityToken(
                issuer: Startup.configuration["Jwt:Iss"],
                audience: Startup.configuration["Jwt:Aud"],
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                expires: DateTime.UtcNow.AddHours(5),
                claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}

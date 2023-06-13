using Microsoft.Build.Framework;
using Microsoft.IdentityModel.Tokens;
using MNSGamesWebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MNSGamesWebAPI.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(AppUser appUser)
        {
            var claims = new[]
            {
                new Claim("Email", appUser.Email),
                new Claim("Id", appUser.Id.ToString()),
                new Claim(ClaimTypes.Role, appUser.IsAdmin ? "Admin" : "Visitor")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!));
            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: signature
                );

            var JSONToken = new JwtSecurityTokenHandler().WriteToken(token);
            return JSONToken;
        }
    }
}
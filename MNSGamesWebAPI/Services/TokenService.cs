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
        private readonly MNS_Games_DBContext _context;

        public TokenService(IConfiguration configuration, MNS_Games_DBContext context)
        {
            _configuration = configuration;
            _context = context;
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
                    expires: DateTime.UtcNow.AddMinutes(1),
                    signingCredentials: signature
                );

            var JSONToken = new JwtSecurityTokenHandler().WriteToken(token);
            return JSONToken;
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

            var validationParameters = new TokenValidationParameters
            {
                ValidIssuer = _configuration["JWT:Issuer"],
                ValidAudience = _configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                SecurityToken validatedToken;
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

                // Check if token expired
                if (validatedToken.ValidTo < DateTime.UtcNow)
                {
                    return false;
                }

                // Check if user exists in database
                var userIdClaim = claimsPrincipal.FindFirst("Id");
                if (userIdClaim == null || !IsUserInDatabase(userIdClaim.Value))
                {
                    return false;
                }

                return true;
            }
            catch (Exception)
            {
                // Throw exception if token is invalid
                return false;
            }
        }

        private bool IsUserInDatabase(string userId)
        {
            int parsedUserId;
            if (int.TryParse(userId, out parsedUserId))
            {
                var user = _context.AppUsers.FindAsync(parsedUserId);
                return user != null;
            }

            return false;
        }
    }
}
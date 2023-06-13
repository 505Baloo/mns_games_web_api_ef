using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MNSGamesWebAPI.Models;
using MNSGamesWebAPI.Models.DTO;
using MNSGamesWebAPI.Services;
using static BCrypt.Net.BCrypt;


namespace MNSGamesWebAPI.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MNS_Games_DBContext _context;
        private readonly TokenService _tokenService;

        public LoginController(MNS_Games_DBContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> LogAppUser(LoginAppUserDTO loginAppUserDTO, CancellationToken cancelToken)
        {
            if (_context.AppUsers == null)
            {
                return Problem("Entity set 'MNS_Games_DBContext.AppUsers' is null.");
            }

            var appUserToReturn = await _context.AppUsers.FirstOrDefaultAsync(appUser => appUser.Email == loginAppUserDTO.Email, cancelToken);

            if (appUserToReturn == null)
                return BadRequest("User not found!");

            var tempPassword = appUserToReturn.LoginPassword;
            if(!Verify(loginAppUserDTO.LoginPassword, tempPassword))
            {
                return BadRequest("Password doesn't match!");
            }

            string token = _tokenService.GenerateToken(appUserToReturn);

            return Ok(token);
        }
    }
}

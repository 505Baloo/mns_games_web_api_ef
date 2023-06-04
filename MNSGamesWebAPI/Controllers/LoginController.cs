using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MNSGamesWebAPI.Models;
using MNSGamesWebAPI.Models.DTO;
using static BCrypt.Net.BCrypt;


namespace MNSGamesWebAPI.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MNS_Games_DBContext _context;

        public LoginController(MNS_Games_DBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<AppUser>> LogAppUser(LoginAppUserDTO loginAppUserDTO, CancellationToken cancelToken)
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
            return Ok(appUserToReturn);
        }
    }
}

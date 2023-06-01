using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<AppUser>> LogAppUser(LoginAppUserDTO loginAppUserDTO)
        {
            if (_context.AppUsers == null)
            {
                return Problem("Entity set 'MNS_Games_DBContext.AppUsers' is null.");
            }

            var temp = _context.AppUsers.Where(appUser => appUser.LoginNickname == loginAppUserDTO.LoginNickname || appUser.Email == loginAppUserDTO.Email).FirstOrDefault();
            AppUser appUserToReturn = null;
            

            if (temp == null)
                return BadRequest("User not found!");
            else
            {
                var tempPassword = temp.LoginPassword;
                if(Verify(loginAppUserDTO.LoginPassword, tempPassword))
                {
                    appUserToReturn = temp;
                }
            }

            return Ok(appUserToReturn);
        }
    }
}

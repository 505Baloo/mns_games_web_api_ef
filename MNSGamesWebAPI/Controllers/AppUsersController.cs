using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MNSGamesWebAPI.Models;
using MNSGamesWebAPI.Models.DTO;
using static BCrypt.Net.BCrypt;

namespace MNSGamesWebAPI.Controllers
{
    [Route("api/AppUser")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly MNS_Games_DBContext _context;

        public AppUsersController(MNS_Games_DBContext context)
        {
            _context = context;
        }

        // GET: api/AppUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUserDTO>>> GetAppUsers()
        {
            if (_context.AppUsers == null)
            {
                return NotFound();
            }
            var appUsers = await _context.AppUsers.ToListAsync();
            return appUsers.Select(appUser => new AppUserDTO(appUser)).ToList();
        }

        // GET: api/AppUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetAppUser(int id)
        {
          if (_context.AppUsers == null)
          {
              return NotFound();
          }
            var appUser = await _context.AppUsers.FindAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            return appUser;
        }

        // PUT: api/AppUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppUser(int id, AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(appUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AppUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AppUser>> PostAppUser(SignUpAppUserDTO signUpDTO)
        {
            if (_context.AppUsers == null)
            {
                return Problem("Entity set 'MNS_Games_DBContext.AppUsers' is null.");
            }

            bool isLoginNicknameExisting = _context.AppUsers.Any(appUser => appUser.LoginNickname == signUpDTO.LoginNickname);
            bool isEmailExisting = _context.AppUsers.Any(appUser => appUser.Email == signUpDTO.Email);

            if (isLoginNicknameExisting)
                return BadRequest("Login is already taken!");

            if (isEmailExisting)
                return BadRequest("Email already exists!");

            var hashedPassword = HashPassword(signUpDTO.LoginPassword);
            signUpDTO.LoginPassword = hashedPassword;

            AppUser appUserToAdd = signUpDTO.ToAppUser();

            _context.AppUsers.Add(appUserToAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppUser", new { id = appUserToAdd.Id }, appUserToAdd);
        }

        // DELETE: api/AppUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppUser(int id)
        {
            if (_context.AppUsers == null)
            {
                return NotFound();
            }
            var appUser = await _context.AppUsers.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            _context.AppUsers.Remove(appUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppUserExists(int id)
        {
            return (_context.AppUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MNSGamesWebAPI.Models;
using MNSGamesWebAPI.Models.DTO;

namespace MNSGamesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistratesController : ControllerBase
    {
        private readonly MNS_Games_DBContext _context;

        public RegistratesController(MNS_Games_DBContext context)
        {
            _context = context;
        }

        // GET: api/Registrates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistrateDTO>>> GetRegistrates()
        {
          if (_context.Registrates == null)
          {
              return NotFound();
          }
            var registrates = await _context.Registrates.ToListAsync();
             return registrates.Select(registrate => new RegistrateDTO(registrate)).ToList();
        }

        // GET: api/Registrates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegistrateDTO>> GetRegistrate(int id)
        {
          if (_context.Registrates == null)
          {
              return NotFound();
          }
            var registrate = await _context.Registrates.FindAsync(id);

            if (registrate == null)
            {
                return NotFound();
            }

            RegistrateDTO registrateDTO = new RegistrateDTO
            {
                AppUserId = registrate.AppUserId,
                QuizId = registrate.QuizId,
                RegistrationDate = registrate.RegistrationDate,
            };

            return registrateDTO;
        }

        //// PUT: api/Registrates/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRegistrate(int id, Registrate registrate)
        //{
        //    if (id != registrate.QuizId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(registrate).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RegistrateExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Registrates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Registrate>> PostRegistrate(RegistrateDTO registrateDTO)
        {
          if (_context.Registrates == null)
          {
              return Problem("Entity set 'MNS_Games_DBContext.Registrates' is null.");
          }

            var quiz = await _context.Quizzes.FindAsync(registrateDTO.QuizId);

            if (quiz == null)
                return NotFound();

            var user = await _context.AppUsers.FindAsync(registrateDTO.AppUserId);

            if(user == null)
                return NotFound();

            Registrate registrationToAdd = registrateDTO.ToRegistrate();

            _context.Registrates.Add(registrationToAdd);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RegistrateExists(registrationToAdd.QuizId, registrationToAdd.AppUserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Registration succesfully created");
        }

        // DELETE: api/Registrates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistrate(int id)
        {
            if (_context.Registrates == null)
            {
                return NotFound();
            }
            var registrate = await _context.Registrates.FindAsync(id);
            if (registrate == null)
            {
                return NotFound();
            }

            _context.Registrates.Remove(registrate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegistrateExists(int quizId, int appUserId)
        {
            return _context.Registrates.Any(r => r.QuizId == quizId && r.AppUserId == appUserId);
        }
    }
}

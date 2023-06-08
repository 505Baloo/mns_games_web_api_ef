using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MNSGamesWebAPI.Models;

namespace MNSGamesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObtainsController : ControllerBase
    {
        private readonly MNS_Games_DBContext _context;

        public ObtainsController(MNS_Games_DBContext context)
        {
            _context = context;
        }

        // GET: api/Obtains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Obtain>>> GetObtains()
        {
          if (_context.Obtains == null)
          {
              return NotFound();
          }
            return await _context.Obtains.ToListAsync();
        }

        // GET: api/Obtains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Obtain>> GetObtain(int id)
        {
          if (_context.Obtains == null)
          {
              return NotFound();
          }
            var obtain = await _context.Obtains.FindAsync(id);

            if (obtain == null)
            {
                return NotFound();
            }

            return obtain;
        }

        // PUT: api/Obtains/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutObtain(int id, Obtain obtain)
        {
            if (id != obtain.QuestionId)
            {
                return BadRequest();
            }

            _context.Entry(obtain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObtainExists(id))
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

        // POST: api/Obtains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Obtain>> PostObtain(Obtain obtain)
        {
          if (_context.Obtains == null)
          {
              return Problem("Entity set 'MNS_Games_DBContext.Obtains'  is null.");
          }
            _context.Obtains.Add(obtain);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ObtainExists(obtain.QuestionId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetObtain", new { id = obtain.QuestionId }, obtain);
        }

        // DELETE: api/Obtains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteObtain(int id)
        {
            if (_context.Obtains == null)
            {
                return NotFound();
            }
            var obtain = await _context.Obtains.FindAsync(id);
            if (obtain == null)
            {
                return NotFound();
            }

            _context.Obtains.Remove(obtain);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ObtainExists(int id)
        {
            return (_context.Obtains?.Any(e => e.QuestionId == id)).GetValueOrDefault();
        }
    }
}

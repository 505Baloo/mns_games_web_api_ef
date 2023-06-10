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
    public class BadgesController : ControllerBase
    {
        private readonly MNS_Games_DBContext _context;

        public BadgesController(MNS_Games_DBContext context)
        {
            _context = context;
        }

        // GET: api/Badges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BadgeDTO>>> GetBadges()
        {
          if (_context.Badges == null)
          {
              return NotFound();
          }
            var badges = await _context.Badges.ToListAsync();
            return badges.Select(b => new BadgeDTO(b)).ToList();
        }

        // GET: api/Badges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Badge>> GetBadge(int id)
        {
          if (_context.Badges == null)
          {
              return NotFound();
          }
            var badge = await _context.Badges.FindAsync(id);

            if (badge == null)
            {
                return NotFound();
            }

            return badge;
        }

        // PUT: api/Badges/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBadge(int id, Badge badge)
        {
            if (id != badge.Id)
            {
                return BadRequest();
            }

            _context.Entry(badge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BadgeExists(id))
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

        // POST: api/Badges
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Badge>> PostBadge(BadgeDTO badgeDTO, CancellationToken cancelToken)
        {
          if (_context.Badges == null)
          {
              return Problem("Entity set 'MNS_Games_DBContext.Badges'  is null.");
          }

            bool badgeTitleExisting = await _context.Badges.AnyAsync(badge => badge.Title == badgeDTO.Title, cancelToken);

            if (badgeTitleExisting)
                return BadRequest("Badge title is already taken!");

            Badge badgeToAdd = badgeDTO.ToBadge();

            _context.Badges.Add(badgeToAdd);
            await _context.SaveChangesAsync();

            badgeDTO.Id = badgeToAdd.Id;

            return Ok(badgeDTO);
        }

        // DELETE: api/Badges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBadge(int id)
        {
            if (_context.Badges == null)
            {
                return NotFound();
            }
            var badge = await _context.Badges.FindAsync(id);
            if (badge == null)
            {
                return NotFound();
            }

            _context.Badges.Remove(badge);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BadgeExists(int id)
        {
            return (_context.Badges?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

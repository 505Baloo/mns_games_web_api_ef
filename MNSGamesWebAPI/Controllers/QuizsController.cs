using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MNSGamesWebAPI.Models;
using MNSGamesWebAPI.Models.DTO;
using MNSGamesWebAPI.Services;

namespace MNSGamesWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizsController : ControllerBase
    {
        private readonly MNS_Games_DBContext _context;
        private readonly CascadeDeleteService _cascadeDeleteService;

        public QuizsController(MNS_Games_DBContext context, CascadeDeleteService cascadeDeleteService)
        {
            _context = context;
            _cascadeDeleteService = cascadeDeleteService;
        }

        // GET: api/Quizs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDTO>>> GetQuizzes()
        {
          if (_context.Quizzes == null)
          {
              return NotFound();
          }
            var quizzes = await _context.Quizzes.ToListAsync();

            var quizDTOs = quizzes.Select(q => new QuizDTO
            {
                Id = q.Id,
                QuizName = q.QuizName,
                Duration = q.Duration,
                ThemeId = q.ThemeId,
                AppUserId = q.AppUserId
            }).ToList();

        return quizDTOs;
        }

        // GET: api/Quizs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizDTO>> GetQuiz(int id)
        {
          if (_context.Quizzes == null)
          {
              return NotFound();
          }
            var quiz = await _context.Quizzes.FindAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            var quizDTO = new QuizDTO
            {
                Id = quiz.Id,
                QuizName = quiz.QuizName,
                Duration = quiz.Duration,
                ThemeId = quiz.ThemeId,
                AppUserId = quiz.AppUserId,
            };

            return quizDTO;
        }

        // PUT: api/Quizs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(int id, Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return BadRequest();
            }

            _context.Entry(quiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizExists(id))
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

        // POST: api/Quizs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuizDTO>> PostQuiz(QuizDTO quizDTO)
        {
          if (_context.Quizzes == null)
          {
              return Problem("Entity set 'MNS_Games_DBContext.Quizzes' is null.");
          }

            var appUser = await _context.AppUsers.FindAsync(quizDTO.AppUserId);

            var theme = await _context.Themes.FindAsync(quizDTO.ThemeId);

            if (appUser == null)
                return NotFound();

            if (theme == null)
                return NotFound();

            Quiz quizToAdd = quizDTO.ToQuiz();

            if(quizDTO.BadgeIds != null)
            {
                var badges = await _context.Badges.Where(badge => quizDTO.BadgeIds.Contains(badge.Id)).ToListAsync();
                foreach (var badge in badges)
                {
                    quizToAdd.Badges.Add(badge);
                }
            }

            _context.Quizzes.Add(quizToAdd);
            await _context.SaveChangesAsync();

            quizDTO.Id = quizToAdd.Id;

            return Ok(quizDTO);
        }

        // DELETE: api/Quizs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            if (_context.Quizzes == null)
            {
                return NotFound();
            }
            var quiz = await _context.Quizzes.FindAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            //_context.Questions.RemoveRange(quiz.Questions);
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuizExists(int id)
        {
            return (_context.Quizzes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

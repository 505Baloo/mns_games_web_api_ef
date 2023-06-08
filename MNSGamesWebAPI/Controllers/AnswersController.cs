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
    public class AnswersController : ControllerBase
    {
        private readonly MNS_Games_DBContext _context;

        public AnswersController(MNS_Games_DBContext context)
        {
            _context = context;
        }

        // GET: api/Answers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerDTO>>> GetAnswers()
        {
          if (_context.Answers == null)
          {
              return NotFound();
          }
            var answers = await _context.Answers.ToListAsync();

            var answersDTO = answers.Select(answer => new AnswerDTO
            {
                Id = answer.Id,
                LabelAnswer = answer.LabelAnswer,
                IsCorrect = answer.IsCorrect,
                Points = answer.Points,
                QuestionId = answer.QuestionId,
            }).ToList();

            return answersDTO;
        }

        // GET: api/Answers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerDTO>> GetAnswer(int id)
        {
          if (_context.Answers == null)
          {
              return NotFound();
          }
            var answer = await _context.Answers.FindAsync(id);

            if (answer == null)
            {
                return NotFound();
            }

            var answerDTO = new AnswerDTO
            {
                Id = answer.Id,
                LabelAnswer = answer.LabelAnswer,
                IsCorrect = answer.IsCorrect,
                Points = answer.Points,
                QuestionId = answer.QuestionId,
            };

            return answerDTO;
        }

        // PUT: api/Answers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswer(int id, Answer answer)
        {
            if (id != answer.Id)
            {
                return BadRequest();
            }

            _context.Entry(answer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerExists(id))
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

        // POST: api/Answers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Answer>> PostAnswer(AnswerDTO answerDTO)
        {
          if (_context.Answers == null)
          {
              return Problem("Entity set 'MNS_Games_DBContext.Answers'  is null.");
          }
            // TODO: Verify LabelAnswer doesn't exist in DB, if it does, use that one instead and assign it to another question (many-to-many)
            var question = await _context.Questions.FindAsync(answerDTO.QuestionId);

            if (question == null)
                return NotFound();

            Answer answerToAdd = answerDTO.ToAnswer();

            _context.Answers.Add(answerToAdd);
            // Test 
            question.AnswersNavigation.Add(answerToAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnswer", new { id = answerToAdd.Id }, answerToAdd);
        }

        // DELETE: api/Answers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            if (_context.Answers == null)
            {
                return NotFound();
            }
            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnswerExists(int id)
        {
            return (_context.Answers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

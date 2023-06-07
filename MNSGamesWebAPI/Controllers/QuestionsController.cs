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
    public class QuestionsController : ControllerBase
    {
        private readonly MNS_Games_DBContext _context;

        public QuestionsController(MNS_Games_DBContext context)
        {
            _context = context;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDTO>>> GetQuestions()
        {

            if (_context.Questions == null)
                return NotFound();

            var questions = _context.Questions.ToList();

            var questionsDTOs = questions.Select(question => new QuestionDTO
            {
                Id= question.Id,
                LabelQuestion = question.LabelQuestion,
                Duration = question.Duration,
                QuizId = question.QuizId,
            }).ToList();

            return questionsDTOs;
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDTO>> GetQuestion(int id)
        {
          if (_context.Questions == null)
          {
              return NotFound();
          }
            var question = await _context.Questions.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            QuestionDTO questionDTO = new QuestionDTO
            {
                Id = question.Id,
                LabelQuestion = question.LabelQuestion,
                Duration = question.Duration,
                QuizId = question.QuizId,
            };

            return questionDTO;
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id, Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }

            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
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

        // POST: api/Questions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(QuestionDTO questionDTO)
        {
          if (_context.Questions == null)
          {
              return Problem("Entity set 'MNS_Games_DBContext.Questions'  is null.");
          }

            var quiz = await _context.Quizzes.FindAsync(questionDTO.QuizId);

            if (quiz == null)
                return NotFound();

            Question questionToAdd = questionDTO.ToQuestion();

            _context.Questions.Add(questionToAdd);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestion", new { id = questionToAdd.Id }, questionToAdd);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            if (_context.Questions == null)
            {
                return NotFound();
            }

            var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionExists(int id)
        {
            return (_context.Questions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

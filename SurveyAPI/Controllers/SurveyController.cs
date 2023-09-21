using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveyAPI.DTO;

namespace SurveyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SurveyController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SurveyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SurveyQuestions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SurveyQuestion>>> GetSurveyQuestions()
        {
            if (_context.SurveyQuestions == null)
            {
                return NotFound();
            }
            return await _context.SurveyQuestions.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyQuestion>> GetSurveyQuestion(int id)
        {
            if (_context.SurveyQuestions == null)
            {
                return NotFound();
            }

            var question = await _context.SurveyQuestions.FindAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        [HttpPost]
        public async Task<ActionResult<SurveyAnswer>> PostSurvey(List<SurveyAnswer> answers)
        {
            try
            {
                // Create a new survey entry in the Surveys table with Malaysia time
                var malaysiaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Kuala_Lumpur");
                var malaysiaTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, malaysiaTimeZone);

                var survey = new Survey
                {
                    SubmitTime = malaysiaTime, // Set the submission time to Malaysia time
                                               
                };

                _context.Surveys.Add(survey);
                await _context.SaveChangesAsync();

                // Save the survey answer data to the SurveyAnswers table
                foreach (var answerData in answers)
                {
                    var surveyAnswer = new SurveyAnswer
                    {
                        SurveyId = survey.Id,
                        SurveyQuestionId = answerData.SurveyQuestionId,
                        Answer = answerData.Answer,
                    };
                    _context.SurveyAnswers.Add(surveyAnswer);
                }
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Survey submitted successfully", SurveyId = survey.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SurveyQuestion>> PutSurveyQuestion(int id, SurveyQuestion updatedQuestion)
        {
            try
            {
                var existingQuestion = await _context.SurveyQuestions.FindAsync(id);

                if (existingQuestion == null)
                {
                    return NotFound(); // If the question with the given ID is not found, return a 404 Not Found response.
                }

                // Update the existing question properties with the values from the updatedQuestion.
                existingQuestion.Question = updatedQuestion.Question; 

                _context.SurveyQuestions.Update(existingQuestion);
                await _context.SaveChangesAsync();

                return Ok(existingQuestion); // Return the updated question.
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SurveyQuestion>> DeleteSurveyQuestion(int id)
        {
            try
            {
                var questionToDelete = await _context.SurveyQuestions.FindAsync(id);

                if (questionToDelete == null)
                {
                    return NotFound(); // If the question with the given ID is not found, return a 404 Not Found response.
                }

                _context.SurveyQuestions.Remove(questionToDelete);
                await _context.SaveChangesAsync();

                return Ok(questionToDelete); // Return the deleted question.
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Error = ex.Message });
            }
        }

        [HttpGet("submission-history")]
        public async Task<ActionResult<IEnumerable<SurveySubmission>>> GetSubmissionHistory()
        {
            try
            {
                var submissionHistory = await _context.SurveyAnswers
                    .Join(
                        _context.Surveys,
                        answer => answer.SurveyId,
                        survey => survey.Id,
                        (answer, survey) => new
                        {
                            Answer = answer,
                            Survey = survey
                        }
                    )
                    .ToListAsync();

                var submissionHistoryDTO = submissionHistory.Select(entry => new SurveySubmission
                {
                    Id = entry.Answer.Id,
                    SubmitTime = entry.Survey.SubmitTime,
                    Answers = entry.Answer.Answer,

                }).ToList();

                return Ok(submissionHistoryDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error", Error = ex.Message });
            }
        }

    }
}

namespace SurveyAPI.DTO
{
    public class SurveySubmission
    {
        public int Id { get; set; }
        public DateTime SubmitTime { get; set; }
        public string? Answers { get; set; }
    }
}
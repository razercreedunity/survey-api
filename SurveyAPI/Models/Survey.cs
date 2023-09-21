public class SurveyQuestion
{
    public int Id { get; set; }
    public string? Question { get; set; }
}

public class Survey
{
    public int Id { get; set; }
    public DateTime SubmitTime { get; set; }
    // Add more properties as needed
}

public class SurveyAnswer
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    public int SurveyQuestionId { get; set; }
    public string? Answer { get; set; }
}
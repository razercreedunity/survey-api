using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
    public DbSet<Survey> Surveys { get; set; }
    public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
}
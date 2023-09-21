using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNameSurveyQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "SurveyAnswers");

            migrationBuilder.RenameColumn(
                name: "QuestionText",
                table: "SurveyQuestions",
                newName: "Question");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Question",
                table: "SurveyQuestions",
                newName: "QuestionText");

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "SurveyAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

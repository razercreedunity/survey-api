using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFKQuestionID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SurveyQuestionId",
                table: "SurveyAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SurveyQuestionId",
                table: "SurveyAnswers");
        }
    }
}

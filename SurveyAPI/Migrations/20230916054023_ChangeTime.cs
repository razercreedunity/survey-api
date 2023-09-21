using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SurveyAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Surveys",
                newName: "SubmitTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubmitTime",
                table: "Surveys",
                newName: "CreatedAt");
        }
    }
}

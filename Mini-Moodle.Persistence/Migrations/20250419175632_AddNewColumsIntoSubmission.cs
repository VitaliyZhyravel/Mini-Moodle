using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mini_Moodle.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumsIntoSubmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Grades_SubmissionId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "IsGraded",
                table: "Submissions");

            migrationBuilder.AddColumn<double>(
                name: "Grade",
                table: "Submissions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_SubmissionId",
                table: "Grades",
                column: "SubmissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Grades_SubmissionId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Submissions");

            migrationBuilder.AddColumn<bool>(
                name: "IsGraded",
                table: "Submissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_SubmissionId",
                table: "Grades",
                column: "SubmissionId",
                unique: true);
        }
    }
}

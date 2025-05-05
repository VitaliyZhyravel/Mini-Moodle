using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mini_Moodle.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeToExpi",
                table: "Difficulties",
                newName: "DaysToExpire");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DaysToExpire",
                table: "Difficulties",
                newName: "TimeToExpi");
        }
    }
}

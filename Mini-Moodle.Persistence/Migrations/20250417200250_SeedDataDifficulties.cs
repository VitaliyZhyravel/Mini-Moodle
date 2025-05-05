using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mini_Moodle.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataDifficulties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("5346433e-8902-40f2-8dde-03680bd679e3"),
                column: "DaysToExpire",
                value: 8.0);

            migrationBuilder.UpdateData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("53e0a3bf-00f0-4bbb-a789-9fd2fe7cf50c"),
                column: "DaysToExpire",
                value: 5.0);

            migrationBuilder.UpdateData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("59483b19-5283-4800-9ab9-07cbdd37aa19"),
                column: "DaysToExpire",
                value: 2.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("5346433e-8902-40f2-8dde-03680bd679e3"),
                column: "DaysToExpire",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("53e0a3bf-00f0-4bbb-a789-9fd2fe7cf50c"),
                column: "DaysToExpire",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("59483b19-5283-4800-9ab9-07cbdd37aa19"),
                column: "DaysToExpire",
                value: 0.0);
        }
    }
}

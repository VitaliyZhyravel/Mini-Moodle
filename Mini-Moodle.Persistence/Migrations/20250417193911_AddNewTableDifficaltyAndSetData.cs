using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mini_Moodle.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTableDifficaltyAndSetData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DifficultyId",
                table: "Assignments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Difficulties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulties", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5346433e-8902-40f2-8dde-03680bd679e3"), "Hard" },
                    { new Guid("53e0a3bf-00f0-4bbb-a789-9fd2fe7cf50c"), "Medium" },
                    { new Guid("59483b19-5283-4800-9ab9-07cbdd37aa19"), "Easy" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_DifficultyId",
                table: "Assignments",
                column: "DifficultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Difficulties_DifficultyId",
                table: "Assignments",
                column: "DifficultyId",
                principalTable: "Difficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Difficulties_DifficultyId",
                table: "Assignments");

            migrationBuilder.DropTable(
                name: "Difficulties");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_DifficultyId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "DifficultyId",
                table: "Assignments");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mini_Moodle.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1b04ab86-be5b-4fa6-a460-1951c2ae6370"), null, "Admin", "ADMIN" },
                    { new Guid("42331567-9bd0-44df-a3ed-c41342e65298"), null, "Student", "STUDENT" },
                    { new Guid("eecb09e1-5951-47d0-ae4e-8c28587176d4"), null, "Teacher", "TEACHER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1b04ab86-be5b-4fa6-a460-1951c2ae6370"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("42331567-9bd0-44df-a3ed-c41342e65298"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("eecb09e1-5951-47d0-ae4e-8c28587176d4"));
        }
    }
}

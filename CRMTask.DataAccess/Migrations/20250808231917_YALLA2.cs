using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CRMTask.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class YALLA2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "IsHCP", "LastName", "Phone", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), "john.smith@hospital.com", "Dr. John", true, "Smith", "+1234567890", new DateTime(2025, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, new DateTime(2025, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc), "george.issac2003@gmail.com", "Dr. Sarah", true, "Johnson", "+1234567891", new DateTime(2025, 8, 9, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }
    }
}

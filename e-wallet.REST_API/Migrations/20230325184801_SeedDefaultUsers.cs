using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace e_wallet.REST_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Balance", "ConcurrencyStamp", "Email", "EmailConfirmed", "Identified", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("495d5ca1-fd83-47d7-971d-59ac74f194b1"), 0, 0.0, "c2cb26c1-fbc8-471e-bda0-01ddbe9fc904", null, false, false, false, null, null, null, null, null, false, null, false, "Admin" },
                    { new Guid("577e632b-3ea5-4c84-a677-755641dae635"), 0, 0.0, "2b0e4c21-435e-4b3f-ab08-1035f499a00d", null, false, false, false, null, null, null, null, null, false, null, false, "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("495d5ca1-fd83-47d7-971d-59ac74f194b1"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("577e632b-3ea5-4c84-a677-755641dae635"));
        }
    }
}

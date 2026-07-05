using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProfileService.Migrations
{
    /// <inheritdoc />
    public partial class AddMockSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "CreatedById", "CreatedOn", "Email", "FirstName", "IsActive", "IsDeleted", "IsPremiumCached", "LastName", "PhoneNumber", "ProfilePictureUrl", "UpdatedById", "UpdatedOn" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), "test@developer.com", "Hazem", true, false, false, "Mofdi", "", "https://example.com/avatar.png", null, null });

            migrationBuilder.InsertData(
                table: "UserStatisticsCache",
                columns: new[] { "Id", "CreatedById", "CreatedOn", "CurrentStreak", "IsActive", "IsDeleted", "LastSyncedAt", "TotalWorkouts", "UpdatedById", "UpdatedOn" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), 5, true, false, null, 25, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "UserStatisticsCache",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }
    }
}

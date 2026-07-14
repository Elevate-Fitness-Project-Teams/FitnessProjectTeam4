using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessCalculationEngine.Common.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class HardenActivePlanInvariant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFitnessStats_UserId",
                table: "UserFitnessStats");

            migrationBuilder.DropIndex(
                name: "IX_UserAssignedPlans_UserId",
                table: "UserAssignedPlans");

            migrationBuilder.AlterColumn<decimal>(
                name: "Weight",
                table: "UserFitnessStats",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Height",
                table: "UserFitnessStats",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "MinCalorie",
                table: "FitnessPlanConfigs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxCalorie",
                table: "FitnessPlanConfigs",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Tdee",
                table: "CalculatedMetrics",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "CalorieTarget",
                table: "CalculatedMetrics",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Bmr",
                table: "CalculatedMetrics",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateIndex(
                name: "IX_UserFitnessStats_UserId_RecordedAt",
                table: "UserFitnessStats",
                columns: new[] { "UserId", "RecordedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignedPlans_UserId",
                table: "UserAssignedPlans",
                column: "UserId",
                unique: true,
                filter: "[IsActive] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignedPlans_UserId_IsActive",
                table: "UserAssignedPlans",
                columns: new[] { "UserId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_FitnessPlanConfigs_Goal_Status",
                table: "FitnessPlanConfigs",
                columns: new[] { "Goal", "Status" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserFitnessStats_UserId_RecordedAt",
                table: "UserFitnessStats");

            migrationBuilder.DropIndex(
                name: "IX_UserAssignedPlans_UserId",
                table: "UserAssignedPlans");

            migrationBuilder.DropIndex(
                name: "IX_UserAssignedPlans_UserId_IsActive",
                table: "UserAssignedPlans");

            migrationBuilder.DropIndex(
                name: "IX_FitnessPlanConfigs_Goal_Status",
                table: "FitnessPlanConfigs");

            migrationBuilder.AlterColumn<double>(
                name: "Weight",
                table: "UserFitnessStats",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Height",
                table: "UserFitnessStats",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "MinCalorie",
                table: "FitnessPlanConfigs",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "MaxCalorie",
                table: "FitnessPlanConfigs",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Tdee",
                table: "CalculatedMetrics",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "CalorieTarget",
                table: "CalculatedMetrics",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "Bmr",
                table: "CalculatedMetrics",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_UserFitnessStats_UserId",
                table: "UserFitnessStats",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAssignedPlans_UserId",
                table: "UserAssignedPlans",
                column: "UserId");
        }
    }
}

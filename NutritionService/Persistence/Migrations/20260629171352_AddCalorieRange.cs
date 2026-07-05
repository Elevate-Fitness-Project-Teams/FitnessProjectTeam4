using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutritionService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCalorieRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetCalorieRangeMax",
                table: "MealPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TargetCalorieRangeMin",
                table: "MealPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetCalorieRangeMax",
                table: "MealPlans");

            migrationBuilder.DropColumn(
                name: "TargetCalorieRangeMin",
                table: "MealPlans");
        }
    }
}

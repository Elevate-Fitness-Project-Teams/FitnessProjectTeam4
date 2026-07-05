using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutritionService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCalorieTarget : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalorieTarget",
                table: "MealPlans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CalorieTarget",
                table: "MealPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

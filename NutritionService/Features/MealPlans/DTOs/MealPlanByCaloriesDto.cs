namespace NutritionService.Features.MealPlans.DTOs
{
    public class MealPlanByCaloriesDto
    {
        public Guid MealPlanId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int TargetCalorieRangeMin { get; set; }

        public int TargetCalorieRangeMax { get; set; }
    }
}

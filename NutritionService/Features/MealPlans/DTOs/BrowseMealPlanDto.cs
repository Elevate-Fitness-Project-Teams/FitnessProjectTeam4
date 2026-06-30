namespace NutritionService.Features.MealPlans.DTOs
{
    public class BrowseMealPlanDto
    {
        public Guid MealPlanId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TargetCalorieRangeMin { get; set; }
        public int TargetCalorieRangeMax { get; set; }
        public List<MealPlanItemDto> Items { get; set; } = new();
    }
}

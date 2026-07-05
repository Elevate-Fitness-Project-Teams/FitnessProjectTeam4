namespace NutritionService.Features.MealPlans.DTOs
{
    public class MealPlanItemDto
    {
        public Guid Id { get; set; }
        public Guid MealId { get; set; }
        public string MealName { get; set; } = string.Empty;
        public string DayOfWeek { get; set; } = string.Empty;
        public string MealTime { get; set; } = string.Empty;
    }
}

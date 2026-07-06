namespace NutritionService.Features.Recommendations.DTOs
{
    public class RecommendedMealDto
    {
        public Guid MealId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MealType { get; set; } = string.Empty;
        public int Calories { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public float Protein { get; set; }

    }
}

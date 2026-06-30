namespace NutritionService.Features.Recommendations.DTOs
{
    public class RecommendedMealDto
    {
        public Guid MealId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Calories { get; set; }
        public float Protein { get; set; }
        public float Carbs { get; set; }
        public float Fats { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}

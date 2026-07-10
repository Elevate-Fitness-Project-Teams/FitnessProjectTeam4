using NutritionService.Common;

namespace NutritionService.Features.Recommendations.DTOs
{
    public class RecommendationResponseDto
    {
        public int UserDailyGoalCalories { get; set; }
        public List<RecommendedMealDto> RecommendedMeals { get; set; } = new();
    }
}

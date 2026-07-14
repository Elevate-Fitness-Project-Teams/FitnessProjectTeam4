using NutritionService.Domain.Entities;
using NutritionService.Domain.Enums;
using NutritionService.Features.Recommendations.DTOs;

namespace NutritionService.Features.Recommendations.Extensions
{
    public static class MealFilterExtensions
    {
        public static IQueryable<RecommendedMealDto> ApplyRecommendationsFilter(
            this IQueryable<Meal> query,
            string? mealType,
            int? maxCalories,
            float? minProtein)
        {
            if (!string.IsNullOrEmpty(mealType)
                && Enum.TryParse<MealType>(mealType, true, out var mealTypeEnum))
            {
                query = query.Where(m => m.Type == mealTypeEnum);
            }

            if (maxCalories.HasValue)
            {
                query = query.Where(m => m.NutritionFacts != null && m.NutritionFacts.Calories <= maxCalories.Value);
            }

            if (minProtein.HasValue)
            {
                query = query.Where(m => m.NutritionFacts != null && m.NutritionFacts.Protein >= minProtein.Value);
            }

            return query.Select(m => new RecommendedMealDto
            {
                MealId = m.MealId,
                Name = m.Name,
                MealType = m.Type.ToString(),
                Calories = m.NutritionFacts != null ? m.NutritionFacts.Calories : 0,
                ImageUrl = m.ImageUrl ?? string.Empty,
                Protein = m.NutritionFacts != null ? m.NutritionFacts.Protein : 0f
            });
        }
    }
}

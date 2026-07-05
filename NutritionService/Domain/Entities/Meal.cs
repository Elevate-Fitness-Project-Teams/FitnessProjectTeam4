using NutritionService.Domain.Common;
using NutritionService.Domain.Enums;
namespace NutritionService.Domain.Entities
{
    public class Meal : BaseEntity
    {
        public Guid MealId { get; set; }
        public string Name { get; set; } = string.Empty;
        public MealType Type { get; set; }
        public int PrepTimeInMinutes { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public string? ImageUrl { get; set; }
        public string IngredientsJson { get; set; } = string.Empty;
        public string InstructionsJson { get; set; } = string.Empty;
        public string? AllergensJson { get; set; }
        public string? TagsJson { get; set; }
        public string? VariationsJson { get; set; }


        public NutritionFacts? NutritionFacts { get; set; }
        public ICollection<MealPlanItem> MealPlanItems { get; set; } = new List<MealPlanItem>();
    }
}

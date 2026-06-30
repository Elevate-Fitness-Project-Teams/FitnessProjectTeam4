using NutritionService.Domain.Common;
namespace NutritionService.Domain.Entities
{
    public class NutritionFacts : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid MealId { get; set; }
        public int Calories { get; set; }
        public float Protein { get; set; }
        public float Carbs { get; set; }
        public float Fats { get; set; }
        public float Fiber { get; set; }
      
        public Meal Meal { get; set; } = null!;

    }
}

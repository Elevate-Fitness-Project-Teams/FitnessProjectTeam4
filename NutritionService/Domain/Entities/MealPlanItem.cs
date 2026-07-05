using NutritionService.Domain.Common;
using NutritionService.Domain.Enums;
namespace NutritionService.Domain.Entities
{
    public class MealPlanItem : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid MealPlanId { get; set; }
        public Guid MealId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public MealType MealTime { get; set; }
       
        public MealPlan MealPlan { get; set; } = null!;
        public Meal Meal { get; set; } = null!;
    }
}

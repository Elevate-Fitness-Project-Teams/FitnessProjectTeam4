using NutritionService.Domain.Common;
namespace NutritionService.Domain.Entities
{
    public class MealPlan : BaseEntity
    {
        public Guid MealPlanId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int TargetCalorieRangeMin { get; set; }
        public int TargetCalorieRangeMax { get; set; }
        
        public ICollection<MealPlanItem> MealPlanItems { get; set; } = new List<MealPlanItem>();
    }
}

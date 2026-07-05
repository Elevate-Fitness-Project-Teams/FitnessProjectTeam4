using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutritionService.Domain.Entities;

namespace NutritionService.Persistence.Configurations
{
    public class MealPlanConfiguration : IEntityTypeConfiguration<MealPlan>
    {
        public void Configure(EntityTypeBuilder<MealPlan> builder)
        {
            builder.ToTable("MealPlans");
            builder.HasKey(e => e.MealPlanId);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(500);
            builder.Property(e => e.TargetCalorieRangeMin).IsRequired();
            builder.Property(e => e.TargetCalorieRangeMax).IsRequired();
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}

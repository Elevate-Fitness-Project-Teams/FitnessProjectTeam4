using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NutritionService.Domain.Entities;

namespace NutritionService.Persistence.Configurations
{
    public class MealPlanItemConfiguration : IEntityTypeConfiguration<MealPlanItem>
    {
        public void Configure(EntityTypeBuilder<MealPlanItem> builder)
        {
            builder.ToTable("MealPlanItems");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.DayOfWeek).IsRequired().HasConversion<string>().HasMaxLength(15);
            builder.Property(e => e.MealTime).IsRequired().HasConversion<string>().HasMaxLength(20);
            builder.HasOne(mpi => mpi.MealPlan)
                .WithMany(mp => mp.MealPlanItems)
                .HasForeignKey(mpi => mpi.MealPlanId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(mpi => mpi.Meal)
                .WithMany(m => m.MealPlanItems)
                .HasForeignKey(mpi => mpi.MealId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}

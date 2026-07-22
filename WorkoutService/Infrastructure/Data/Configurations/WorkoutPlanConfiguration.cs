using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutService.Domain.Aggregates.WorkoutPlans;

namespace WorkoutService.Infrastructure.Data.Configurations
{
    public class WorkoutPlanConfiguration : IEntityTypeConfiguration<WorkoutPlan>
    {
        public void Configure(EntityTypeBuilder<WorkoutPlan> builder)
        {
            builder.Property(wp => wp.Id)
                   .ValueGeneratedNever();

            builder.Property(wp => wp.Title)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(wp => wp.Description)
                   .HasMaxLength(1000);

            builder.Property(wp => wp.UserTier)
                   .IsRequired()
                   .HasMaxLength(50);
             
            builder.HasQueryFilter(wp => !wp.IsDeleted);

        }
    }
}

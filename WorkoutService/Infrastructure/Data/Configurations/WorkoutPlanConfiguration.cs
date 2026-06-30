using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutService.Domain.Aggregates.WorkoutPlans;

namespace WorkoutService.Infrastructure.Data.Configurations
{
    public class WorkoutPlanConfiguration : IEntityTypeConfiguration<WorkoutPlan>
    {
        public void Configure(EntityTypeBuilder<WorkoutPlan> builder)
        {
            builder.HasKey(wp => wp.Id);

            builder.Property(wp => wp.Title)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(wp => wp.Description)
                   .HasMaxLength(1000);

            builder.Property(wp => wp.UserTier)
                   .IsRequired()
                   .HasMaxLength(50);
             
            builder.HasQueryFilter(wp => !wp.IsDeleted);

            builder.HasIndex(wp => wp.ExternalPlanId)
                   .IsUnique()
                   .HasDatabaseName("IX_WorkoutPlans_ExternalPlanId");

            builder.HasIndex(wp => wp.Goal)
                   .HasDatabaseName("IX_WorkoutPlans_Goal");

            builder.HasIndex(wp => wp.Status)
                   .HasDatabaseName("IX_WorkoutPlans_Status");

            builder.HasIndex(wp => wp.Difficulty)
                   .HasDatabaseName("IX_WorkoutPlans_Difficulty");


        }
    }
}

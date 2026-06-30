using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutService.Domain.Aggregates.WorkoutPlans;

namespace WorkoutService.Infrastructure.Data.Configurations
{
    public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Name)
               .IsRequired()
               .HasMaxLength(150);

            builder.HasOne<WorkoutPlan>()
                   .WithMany(wp => wp.Workouts)
                   .HasForeignKey(w => w.WorkoutPlanId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(w => w.Category)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(w => w.Difficulty)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(w => w.ImageUrl)
                   .HasMaxLength(500);

            builder.HasIndex(w => w.Difficulty)
                   .HasDatabaseName("IX_Workouts_Difficulty");

            builder.HasIndex(w => w.Category)
                   .HasDatabaseName("IX_Workouts_Category");

            builder.HasIndex(w => w.WorkoutPlanId)
                   .HasDatabaseName("IX_Workouts_WorkoutPlanId");

            builder.HasIndex(w => w.DurationInMinutes)
                   .HasDatabaseName("IX_Workouts_Duration");

            builder.HasIndex(  w => new
            {
                w.Category,
                w.Difficulty,
                w.DurationInMinutes
            }).HasDatabaseName("IX_Workouts_Search");
        }
    }
}

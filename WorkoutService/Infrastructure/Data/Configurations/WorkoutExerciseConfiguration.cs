using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutService.Domain.Aggregates.WorkoutPlans;
using WorkoutService.Domain.References;

namespace WorkoutService.Infrastructure.Data.Configurations
{
    public class WorkoutExerciseConfiguration : IEntityTypeConfiguration<WorkoutExercise>
    {
        public void Configure(EntityTypeBuilder<WorkoutExercise> builder)
        {
            builder.HasKey(we => new { we.WorkoutId, we.ExerciseId });

            builder.Property(we => we.RepsDefault)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasOne<Workout>()
                   .WithMany(w => w.WorkoutExercises)
                   .HasForeignKey(we => we.WorkoutId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Exercise>()
                   .WithMany()
                   .HasForeignKey(we => we.ExerciseId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(we => we.ExerciseId)
                   .HasDatabaseName("IX_WorkoutExercises_ExerciseId");

            builder.HasIndex(we => we.WorkoutId)
                   .HasDatabaseName("IX_WorkoutExercises_WorkoutId");

            builder.HasIndex(we => new
            {
                we.WorkoutId,
                we.OrderIndex
            }).IsUnique();

            builder.HasIndex(we => new
            {
                we.WorkoutId,
                we.OrderIndex
            }).HasDatabaseName("IX_WorkoutExercises_Order");
        }
    }
}

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
            builder.HasKey(x => x.Id);

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
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgressService.Domian.Aggregates.WorkoutLogs;

namespace ProgressService.Infrastructure.Data.Configurations
{
    public class WorkoutLogExerciseConfiguration : IEntityTypeConfiguration<WorkoutLogExercise>
    {
        public void Configure(EntityTypeBuilder<WorkoutLogExercise> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .ValueGeneratedNever();

            builder.Property(e => e.SetsCompleted)
                   .IsRequired();
             
            builder.Property(e => e.RepsCompleted)
                   .IsRequired();

            builder.Property(e => e.WeightUsed)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(e => e.Completed)
                   .IsRequired();
        }
    }
}

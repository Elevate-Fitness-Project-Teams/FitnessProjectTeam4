using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgressService.Domian.Aggregates.WorkoutLogs;

namespace ProgressService.Infrastructure.Data.Configurations
{
    public class WorkoutLogConfiguration : IEntityTypeConfiguration<WorkoutLog>
    {
        public void Configure(EntityTypeBuilder<WorkoutLog> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Id)
                   .ValueGeneratedNever(); 

            builder.Property(w => w.UserId)
                   .IsRequired()
                   .HasMaxLength(450); 

            builder.Property(w => w.Difficulty)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(w => w.Notes)
                   .HasMaxLength(2000); 

            builder.Property(w => w.Rating)
                   .IsRequired();

            builder.Property(w => w.CompletedAt)
                   .IsRequired();

            builder.HasMany(w => w.ExercisesCompleted)
                .WithOne()
                .HasForeignKey(e => e.WorkoutLogId)
                .OnDelete(DeleteBehavior.Cascade); // Deleting the main log automatically deletes its associated exercise details.
        }
    }
}

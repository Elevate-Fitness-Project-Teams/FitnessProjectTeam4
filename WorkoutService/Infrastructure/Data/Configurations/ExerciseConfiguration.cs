using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;
using WorkoutService.Domain.References;

namespace WorkoutService.Infrastructure.Data.Configurations
{
    public class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(150);

            builder.HasIndex(e => e.Name)
                   .IsUnique(); // To prevent repeating the same exercise name

            builder.Property(e => e.TargetMuscles)
                   .HasConversion(v => JsonSerializer
                   .Serialize(v, (JsonSerializerOptions?)null),v => JsonSerializer
                   .Deserialize<List<string>>(v, (JsonSerializerOptions?)null)!);
                  

            builder.HasIndex(e => e.Difficulty)
                   .HasDatabaseName("IX_Exercises_Difficulty");

            builder.HasIndex(e => e.Name)
                   .HasDatabaseName("IX_Exercises_Name");

        }
    }
}

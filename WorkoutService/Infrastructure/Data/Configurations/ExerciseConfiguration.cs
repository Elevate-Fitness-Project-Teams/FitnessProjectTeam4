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

            builder.Property(e => e.TargetMuscles)
                   .HasConversion(v => JsonSerializer
                   .Serialize(v, (JsonSerializerOptions?)null),v => JsonSerializer
                   .Deserialize<List<string>>(v, (JsonSerializerOptions?)null)!);

        }
    }
}

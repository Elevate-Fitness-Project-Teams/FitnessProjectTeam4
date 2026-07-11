using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutService.Domain.Aggregates.WorkoutPlans;
using WorkoutService.Domain.Aggregates.WorkoutSessions;

namespace WorkoutService.Infrastructure.Data.Configurations
{
    public class WorkoutSessionConfiguration : IEntityTypeConfiguration<WorkoutSession>
    {
        public void Configure(EntityTypeBuilder<WorkoutSession> builder)
        {
            builder.HasKey(ws => ws.Id);

            builder.Property(ws => ws.Status)
            .IsRequired()
            .HasMaxLength(50);

            builder.HasOne<Workout>()
                   .WithMany()
                   .HasForeignKey(ws => ws.WorkoutId)
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgressService.Domian.Aggregates.UserStreaks;

namespace ProgressService.Infrastructure.Data.Configurations
{
    public class UserStreakConfiguration : IEntityTypeConfiguration<UserStreak>
    {
        public void Configure(EntityTypeBuilder<UserStreak> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .ValueGeneratedNever();

            builder.Property(s => s.UserId)
                   .IsRequired()
                   .HasMaxLength(450);

            builder.Property(s => s.CurrentStreak)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(s => s.LongestStreak)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(s => s.LastWorkoutDate)
                .IsRequired(false); //It can be Null if it's a new user
        }
    }
}

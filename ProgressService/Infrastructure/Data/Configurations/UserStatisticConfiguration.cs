using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgressService.Domian.Aggregates.UserStatistics;

namespace ProgressService.Infrastructure.Data.Configurations
{
    public class UserStatisticConfiguration : IEntityTypeConfiguration<UserStatistic>
    {
        public void Configure(EntityTypeBuilder<UserStatistic> builder)
        {

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .ValueGeneratedNever();

            builder.Property(u => u.UserId)
                   .IsRequired()
                   .HasMaxLength(450);

            builder.Property(u => u.TotalWorkoutsCompleted)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(u => u.TotalCaloriesBurned)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(u => u.TotalMinutesTrained)
                   .IsRequired()
                   .HasDefaultValue(0);

            builder.Property(u => u.CurrentWeight)
                   .HasColumnType("decimal(5,2)")
                   .HasDefaultValue(0);

            builder.Property(u => u.StartWeight)
                   .HasColumnType("decimal(5,2)")
                   .HasDefaultValue(0);

            builder.Property(u => u.TotalWeightLost)
                   .HasColumnType("decimal(5,2)")
                   .HasDefaultValue(0);

            builder.Property(u => u.UpdatedAt)
                   .IsRequired();

            builder.HasIndex(x => x.UserId)
                   .IsUnique();

            builder.Property(x => x.RowVersion)
                   .IsRowVersion();

        }
    }
}

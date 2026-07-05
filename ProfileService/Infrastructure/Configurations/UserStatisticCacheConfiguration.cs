using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileService.Domain.Entities;

namespace ProfileService.Infrastructure.Configurations
{
    public class UserStatisticCacheConfiguration : IEntityTypeConfiguration<UserStatisticCache>
    {
        public void Configure(EntityTypeBuilder<UserStatisticCache> builder)
        {
            builder.ToTable("UserStatisticsCache");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TotalWorkouts)
                   .HasDefaultValue(0);

            builder.Property(x => x.CurrentStreak)
                   .HasDefaultValue(0);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileService.Domain.Entities;

namespace ProfileService.Infrastructure.Configurations
{
    public class UserPreferencesConfiguration : IEntityTypeConfiguration<UserPreferences>
    {
        public void Configure(EntityTypeBuilder<UserPreferences> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Language).HasMaxLength(10).HasDefaultValue("en");
            builder.Property(p => p.Theme).HasMaxLength(15).HasDefaultValue("light");
            builder.Property(p => p.WeightUnit).HasMaxLength(5).HasDefaultValue("kg");
            builder.Property(p => p.HeightUnit).HasMaxLength(5).HasDefaultValue("cm");
            builder.Property(p => p.DistanceUnit).HasMaxLength(5).HasDefaultValue("km");
        }
    }
}

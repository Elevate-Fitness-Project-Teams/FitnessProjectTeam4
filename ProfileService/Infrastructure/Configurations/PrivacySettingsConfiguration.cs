using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileService.Domain.Entities;

namespace ProfileService.Infrastructure.Configurations
{
    public class PrivacySettingsConfiguration : IEntityTypeConfiguration<PrivacySettings>
    {
        public void Configure(EntityTypeBuilder<PrivacySettings> builder)
        {
            builder.HasKey(p => p.UserId);

            builder.Property(p => p.ProfileVisibility).HasMaxLength(20).HasDefaultValue("private");
            builder.Property(p => p.ShowProgressToFriends).HasDefaultValue(false);
            builder.Property(p => p.AllowDataSharing).HasDefaultValue(false);
        }
    }
}

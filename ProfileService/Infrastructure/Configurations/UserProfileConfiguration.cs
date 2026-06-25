using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileService.Domain.Entities;

namespace ProfileService.Infrastructure.Configurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(50).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(255).IsRequired();
            builder.Property(u => u.PhoneNumber).HasMaxLength(20).IsRequired();
            builder.Property(u => u.ProfilePictureUrl).HasMaxLength(500);
            builder.Property(u => u.IsPremiumCached).IsRequired();


            builder.HasOne(u => u.Preferences)
                .WithOne()
                .HasForeignKey<UserPreferences>(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.NotificationSettings)
                .WithOne()
                .HasForeignKey<NotificationSettings>(n => n.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.PrivacySettings)
                .WithOne()
                .HasForeignKey<PrivacySettings>(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

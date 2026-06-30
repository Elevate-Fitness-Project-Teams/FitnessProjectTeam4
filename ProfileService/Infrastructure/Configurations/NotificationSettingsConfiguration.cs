using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileService.Domain.Entities;

namespace ProfileService.Infrastructure.Configurations
{
    public class NotificationSettingsConfiguration : IEntityTypeConfiguration<NotificationSettings>
    {
        public void Configure(EntityTypeBuilder<NotificationSettings> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.WorkoutReminders).HasDefaultValue(true);
            builder.Property(n => n.MealReminders).HasDefaultValue(true);
            builder.Property(n => n.AchievementAlerts).HasDefaultValue(true);
            builder.Property(n => n.WeeklyReports).HasDefaultValue(true);
            builder.Property(n => n.EmailNotifications).HasDefaultValue(true);
            builder.Property(n => n.PushNotifications).HasDefaultValue(true);
        }
    }
}

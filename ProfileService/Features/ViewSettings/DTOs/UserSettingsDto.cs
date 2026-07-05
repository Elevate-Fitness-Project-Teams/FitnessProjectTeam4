namespace ProfileService.Features.ViewSettings.DTOs
{
    public class UserSettingsDto
    {
        public UserPreferencesDto Preferences { get; set; } = new();
        public NotificationSettingsDto Notifications { get; set; } = new();
        public PrivacySettingsDto Privacy { get; set; } = new();
    }
}

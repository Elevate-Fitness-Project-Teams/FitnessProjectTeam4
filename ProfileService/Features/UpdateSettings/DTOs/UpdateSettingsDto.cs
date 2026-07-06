namespace ProfileService.Features.UpdateSettings.DTOs
{
    public class UpdateSettingsDto
    {
        public UpdatePreferencesDto? Preferences { get; set; }
        public UpdateNotificationsDto? Notifications { get; set; }
        public UpdatePrivacyDto? Privacy { get; set; }
    }
}

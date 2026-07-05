namespace ProfileService.Features.UpdateSettings.ViewModels
{
    public class UpdateSettingsRequestViewModel
    {
        public UpdatePreferencesViewModel? Preferences { get; set; }
        public UpdateNotificationsViewModel? Notifications { get; set; }
        public UpdatePrivacyViewModel? Privacy { get; set; }
    }
}

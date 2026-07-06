namespace ProfileService.Features.ViewSettings.ViewModels
{
    public class ViewSettingsResponseViewModel
    {
        public UserPreferencesViewModel Preferences { get; set; } = new();
        public NotificationSettingsViewModel Notifications { get; set; } = new();
        public PrivacySettingsViewModel Privacy { get; set; } = new();
    }
}

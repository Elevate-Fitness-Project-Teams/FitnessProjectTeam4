namespace ProfileService.Features.ViewSettings.ViewModels
{
    public class PrivacySettingsViewModel
    {
        public string ProfileVisibility { get; set; } = string.Empty;
        public bool ShowProgressToFriends { get; set; }
        public bool AllowDataSharing { get; set; }
    }
}

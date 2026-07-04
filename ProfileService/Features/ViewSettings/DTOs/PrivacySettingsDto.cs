namespace ProfileService.Features.ViewSettings.DTOs
{
    public class PrivacySettingsDto
    {
        public string ProfileVisibility { get; set; } = string.Empty;
        public bool ShowProgressToFriends { get; set; }
        public bool AllowDataSharing { get; set; }
    }
}

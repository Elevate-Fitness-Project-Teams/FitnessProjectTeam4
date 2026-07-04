namespace ProfileService.Features.UpdateSettings.ViewModels
{
    public class UpdatePrivacyViewModel
    {
        public string? ProfileVisibility { get; set; }
        public bool? ShowProgressToFriends { get; set; }
        public bool? AllowDataSharing { get; set; }
    }
}

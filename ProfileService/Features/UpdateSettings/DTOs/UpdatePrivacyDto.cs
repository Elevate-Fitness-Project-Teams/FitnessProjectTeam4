namespace ProfileService.Features.UpdateSettings.DTOs
{
    public class UpdatePrivacyDto
    {
        public string? ProfileVisibility { get; set; }
        public bool? ShowProgressToFriends { get; set; }
        public bool? AllowDataSharing { get; set; }
    }
}

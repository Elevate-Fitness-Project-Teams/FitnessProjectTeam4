using ProfileService.Domain.Common;

namespace ProfileService.Domain.Entities
{
    public class PrivacySettings : BaseEntity
    {
        public string ProfileVisibility { get; set; } = "private";
        public bool ShowProgressToFriends { get; set; } = false;
        public bool AllowDataSharing { get; set; } = false;
    }
}

using ProfileService.Domain.Common;

namespace ProfileService.Domain.Entities
{
    public class UserProfile : BaseEntity
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }
        public bool IsPremiumCached { get; set; }

        #region Navigation Properties
        public UserPreferences? Preferences { get; set; }
        public NotificationSettings? NotificationSettings { get; set; }
        public PrivacySettings? PrivacySettings { get; set; }
        #endregion

    }
}

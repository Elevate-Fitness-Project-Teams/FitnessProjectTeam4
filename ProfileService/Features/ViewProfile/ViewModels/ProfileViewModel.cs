namespace ProfileService.Features.ViewProfile.ViewModels
{
    public class ProfileViewModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }
        public bool IsPremiumCached { get; set; }
        public int TotalWorkouts { get; set; }
        public int CurrentStreak { get; set; }
    }
}

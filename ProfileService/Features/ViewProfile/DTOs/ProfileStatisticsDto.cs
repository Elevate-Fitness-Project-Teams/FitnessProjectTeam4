namespace ProfileService.Features.ViewProfile.DTOs
{
    public class ProfileStatisticsDto
    {
        public Guid UserId { get; set; }
        public int TotalWorkouts { get; set; }
        public int CurrentStreak { get; set; }
    }
}

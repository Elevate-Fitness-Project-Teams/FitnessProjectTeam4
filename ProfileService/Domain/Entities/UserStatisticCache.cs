using ProfileService.Domain.Common;

namespace ProfileService.Domain.Entities
{
    public class UserStatisticCache : BaseEntity
    {

        public int TotalWorkouts { get; set; } = 0;
        public int CurrentStreak { get; set; } = 0;
        public DateTime? LastSyncedAt { get; set; }
    }
}

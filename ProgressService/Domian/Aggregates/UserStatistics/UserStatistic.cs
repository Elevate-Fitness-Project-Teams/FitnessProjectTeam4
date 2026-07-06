namespace ProgressService.Domian.Aggregates.UserStatistics
{
    public class UserStatistic
    {
        public Guid Id { get; private set; }
        public string UserId { get; private set; }
        public int TotalWorkoutsCompleted { get; private set; }
        public int TotalCaloriesBurned { get; private set; }
        public int TotalMinutesTrained { get; private set; }

        private UserStatistic() { }

        public UserStatistic(Guid id, string userId)
        {
            Id = id;
            UserId = userId;
            TotalWorkoutsCompleted = 0;
            TotalCaloriesBurned = 0;
            TotalMinutesTrained = 0;
        }

        public void Accumulate(int minutes, int calories)
        {
            TotalWorkoutsCompleted++;
            TotalMinutesTrained += minutes;
            TotalCaloriesBurned += calories;
        }
    }
}

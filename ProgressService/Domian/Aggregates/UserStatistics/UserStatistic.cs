using ProgressService.Common.Exceptions;
using ProgressService.Common.Responses;

namespace ProgressService.Domian.Aggregates.UserStatistics
{
    public class UserStatistic
    {
        public Guid Id { get; private set; }
        public string UserId { get; private set; }
        public int TotalWorkoutsCompleted { get; private set; }
        public int TotalCaloriesBurned { get; private set; }
        public int TotalMinutesTrained { get; private set; }
        public double CurrentWeight { get; private set; }
        public double StartWeight { get; private set; }
        public double TotalWeightLost { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private UserStatistic() { }

        public UserStatistic(Guid id, string userId)
        {
            Id = id;
            UserId = userId;
            TotalWorkoutsCompleted = 0;
            TotalCaloriesBurned = 0;
            TotalMinutesTrained = 0;
        }

        public UserStatistic(Guid id, string userId, double initialWeight)
        {
            Id = id;
            UserId = userId;
            TotalWorkoutsCompleted = 0;
            TotalCaloriesBurned = 0;
            TotalMinutesTrained = 0;
            StartWeight = initialWeight;
            CurrentWeight = initialWeight;
            TotalWeightLost = 0;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Accumulate(int minutes, int calories)
        {
            TotalWorkoutsCompleted++;
            TotalMinutesTrained += minutes;
            TotalCaloriesBurned += calories;
        }

        public void UpdateWeightMetrics(double newWeight)
        {
            if (newWeight < 40 || newWeight > 200)
                throw new DomainException(ErrorCode.InvalidWeightValue);

            CurrentWeight = newWeight;
            TotalWeightLost = StartWeight - CurrentWeight;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}

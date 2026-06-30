namespace WorkoutService.Domain.Aggregates.WorkoutPlans
{
    public class WorkoutPlan
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ExternalPlanId { get; private set; }
        public string Goal { get; private set; }
        public string Difficulty { get; private set; }
        public string Status { get; private set; }
        public Guid UserId { get; private set; }
        public string UserTier { get; private set; } // Free, Premium
        public DateTime CreatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }


        private readonly List<Workout> _workouts = new();
        public IReadOnlyCollection<Workout> Workouts => _workouts.AsReadOnly();

        private WorkoutPlan() { }

        public WorkoutPlan(string title, string description, Guid userId, string userTier, string externalPlanId, string difficulty, string status)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            UserId = userId;
            ExternalPlanId = externalPlanId;
            Difficulty = difficulty;
            Status = status;
            UserTier = userTier;
            CreatedAt = DateTime.UtcNow;
        }

        public Workout AddWorkout(string name, int duration, string difficulty, string category, int caloriesBurn, string imageUrl, bool isPremium, int dayNumber)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Workout name cannot be empty.");

            if (_workouts.Any(w => w.DayNumber == dayNumber))
                throw new InvalidOperationException($"A workout routine for Day {dayNumber} already exists in this plan.");

            Workout workout = new Workout(name, duration, difficulty, category, caloriesBurn, imageUrl, isPremium, dayNumber);
            _workouts.Add(workout);
            return workout;
        }
        public void MarkAsDeleted()
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }
    }
}

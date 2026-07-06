namespace WorkoutService.Domain.Aggregates.WorkoutLogs
{
    public class WorkoutLog
    {
        public Guid Id { get; private set; } 
        public Guid WorkoutId { get; private set; }
        public Guid SessionId { get; private set; }
        public string UserId { get; private set; }
        public DateTime CompletedAt { get; private set; }
        public int Duration { get; private set; }
        public int CaloriesBurned { get; private set; }
        public string Difficulty { get; private set; }
        public string Notes { get; private set; }
        public int Rating { get; private set; }

        // Nested Collection
        private readonly List<WorkoutLogExercise> _exercisesCompleted = new();
        public IReadOnlyCollection<WorkoutLogExercise> ExercisesCompleted => _exercisesCompleted.AsReadOnly();

        public WorkoutLog(Guid workoutId, Guid sessionId, string userId, DateTime completedAt, int duration, int caloriesBurned, string difficulty, string notes, int rating)
        {
            WorkoutId = workoutId;
            SessionId = sessionId;
            UserId = userId;
            CompletedAt = completedAt;
            Duration = duration;
            CaloriesBurned = caloriesBurned;
            Difficulty = difficulty;
            Notes = notes;
            Rating = rating;
        }

        public void AddCompletedExercise(Guid exerciseId, int sets, int reps, double weight, bool completed)
        {
            _exercisesCompleted.Add(new WorkoutLogExercise(exerciseId, sets, reps, weight, completed));
        }
    }
}

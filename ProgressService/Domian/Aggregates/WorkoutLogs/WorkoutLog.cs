using ProgressService.Common.Exceptions;
using ProgressService.Common.Responses;

namespace ProgressService.Domian.Aggregates.WorkoutLogs
{
    public class WorkoutLog
    {
        public Guid Id { get; private set; }
        public Guid WorkoutId { get; private set; }  // From WorkoutService
        public Guid SessionId { get; private set; }   // From WorkoutService
        public string UserId { get; private set; }
        public DateTime CompletedAt { get; private set; }
        public int DurationInMinutes { get; private set; }
        public int CaloriesBurned { get; private set; }
        public string Difficulty { get; private set; }
        public string? Notes { get; private set; }
        public int Rating { get; private set; }

        private readonly List<WorkoutLogExercise> _exercisesCompleted = new();
        public IReadOnlyCollection<WorkoutLogExercise> ExercisesCompleted => _exercisesCompleted.AsReadOnly();

        private WorkoutLog() { }

        public WorkoutLog(Guid id, Guid workoutId, Guid sessionId, string userId, DateTime completedAt, int durationInMinutes, int caloriesBurned, string difficulty, string? notes, int rating)
        {
            Id = id;
            WorkoutId = workoutId;
            SessionId = sessionId;
            UserId = userId;
            CompletedAt = completedAt;
            DurationInMinutes = durationInMinutes;
            CaloriesBurned = caloriesBurned;
            Difficulty = difficulty;
            Notes = notes;

            if (rating < 1 || rating > 5)
                throw new DomainException(ErrorCode.InvalidWorkoutRating);
            Rating = rating;
        }

        public void AddCompletedExercise(Guid exerciseId, int sets, int reps, decimal weight, bool completed)
        {
            var exerciseLog = new WorkoutLogExercise(Guid.NewGuid(), Id, exerciseId, sets, reps, weight, completed);
            _exercisesCompleted.Add(exerciseLog);
        }
    }
}

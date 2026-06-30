namespace WorkoutService.Domain.Aggregates.WorkoutLogs
{
    public class WorkoutLogExercise
    {
        public Guid Id { get; private set; }
        public Guid WorkoutLogId { get; private set; } // Foreign Key
        public Guid ExerciseId { get; private set; }
        public int SetsCompleted { get; private set; }
        public int RepsCompleted { get; private set; }
        public double WeightUsed { get; private set; }
        public bool Completed { get; private set; }

        public WorkoutLogExercise(Guid exerciseId, int setsCompleted, int repsCompleted, double weightUsed, bool completed)
        {
            ExerciseId = exerciseId;
            SetsCompleted = setsCompleted;
            RepsCompleted = repsCompleted;
            WeightUsed = weightUsed;
            Completed = completed;
        }
    }
}

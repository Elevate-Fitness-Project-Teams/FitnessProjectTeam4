namespace ProgressService.Domian.Aggregates.WorkoutLogs
{
    public class WorkoutLogExercise
    {
        public Guid Id { get; private set; }
        public Guid WorkoutLogId { get; private set; } // Foreign Key 
        public Guid ExerciseId { get; private set; } // From WorkoutService
        public int SetsCompleted { get; private set; }
        public int RepsCompleted { get; private set; }
        public decimal WeightUsed { get; private set; }
        public bool Completed { get; private set; }

        private WorkoutLogExercise() { }

        internal WorkoutLogExercise(Guid id, Guid workoutLogId, Guid exerciseId, int setsCompleted, int repsCompleted, decimal weightUsed, bool completed)
        {
            Id = id;
            WorkoutLogId = workoutLogId;
            ExerciseId = exerciseId;
            SetsCompleted = setsCompleted;
            RepsCompleted = repsCompleted;
            WeightUsed = weightUsed;
            Completed = completed;
        }
    }
}

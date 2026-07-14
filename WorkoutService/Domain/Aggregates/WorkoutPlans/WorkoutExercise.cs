using WorkoutService.Common.Exceptions;
using WorkoutService.Common.Responses;
using WorkoutService.Domain.References;

namespace WorkoutService.Domain.Aggregates.WorkoutPlans
{
    public class WorkoutExercise
    {
        public Guid WorkoutId { get; private set; } // Composite FK 1
        public Guid ExerciseId { get; private set; } // Composite FK 2

        public int OrderIndex { get; private set; }
        public int SetsDefault { get; private set; }
        public string RepsDefault { get; private set; } = null!;
        public int RestTimeInSeconds { get; private set; }
        public Exercise Exercise { get; private set; } = null!;

        private WorkoutExercise() { }

        internal WorkoutExercise(Guid workoutId, Guid exerciseId, int orderIndex, int sets, string reps, int restTimeInSeconds)
        {
            if (sets <= 0) throw new DomainException(ErrorCode.InvalidExerciseSets);
            if (restTimeInSeconds < 0) throw new DomainException(ErrorCode.InvalidExerciseRestTime);

            WorkoutId = workoutId;
            ExerciseId = exerciseId;
            OrderIndex = orderIndex;
            SetsDefault = sets;
            RepsDefault = reps;
            RestTimeInSeconds = restTimeInSeconds;
        }
    }
}

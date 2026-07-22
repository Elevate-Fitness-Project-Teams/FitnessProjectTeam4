namespace WorkoutService.Domain.Aggregates.WorkoutPlans
{
    public class Workout
    {
        public Guid Id { get; private set; }
        public Guid WorkoutPlanId { get; private set; }
        public string Name { get; private set; } = null!;
        public string Category { get; private set; } = null!;
        public string Difficulty { get; private set; } = null!;
        public int DurationInMinutes { get; private set; }
        public int CaloriesBurn { get; private set; }
        public string ImageUrl { get; private set; } = null!;
        public bool IsPremium { get; private set; }
        public int DayNumber { get; private set; }

        private readonly List<WorkoutExercise> _workoutExercises = new();
        public IReadOnlyCollection<WorkoutExercise> WorkoutExercises => _workoutExercises.AsReadOnly();

        private Workout() { }

        internal Workout(Guid workoutPlanId, string name, int durationInMinutes, string difficulty, string category, int caloriesBurn, string imageUrl, bool isPremium, int dayNumber)
        {
            Id = Guid.NewGuid();
            WorkoutPlanId = workoutPlanId;
            Name = name;
            DurationInMinutes = durationInMinutes;
            Difficulty = difficulty;
            Category = category;
            CaloriesBurn = caloriesBurn;
            ImageUrl = imageUrl;
            IsPremium = isPremium;
            DayNumber = dayNumber; 
        }

        public void AddExercise(Guid exerciseId, int sets, string reps, int restTimeInSeconds)
        {
            int nextOrderIndex = _workoutExercises.Count + 1; 

            var workoutExercise = new WorkoutExercise(Id, exerciseId, nextOrderIndex, sets, reps, restTimeInSeconds);
            _workoutExercises.Add(workoutExercise);
        }
    }
}

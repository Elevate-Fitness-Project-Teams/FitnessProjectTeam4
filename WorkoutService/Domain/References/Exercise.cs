namespace WorkoutService.Domain.References
{
    public class Exercise
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } // Bench Press
        public string Description { get; private set; } 
        public string Difficulty { get; private set; } 
        public string VideoUrl { get; private set; } 
        public List<string> TargetMuscles { get; private set; } = new();
        public string EquipmentNeeded { get; private set; } 

        private Exercise() { }

        public Exercise(Guid id, string name, string description, string difficulty, string videoUrl, List<string> targetMuscles, string equipmentNeeded)
        {
            Id = id;
            Name = name;
            TargetMuscles = targetMuscles;
            EquipmentNeeded = equipmentNeeded;
            Description = description;
            Difficulty = difficulty;
            VideoUrl = videoUrl;
        }
    }
}

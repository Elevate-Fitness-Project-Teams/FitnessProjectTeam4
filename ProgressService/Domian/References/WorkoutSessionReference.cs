namespace ProgressService.Domian.References
{
    public class WorkoutSessionReference
    {
        public Guid SessionId { get; set; }
        public Guid UserId { get; set; }
        public Guid WorkoutId { get; set; }
        public DateTime StartTime { get; set; }
        public string Status { get; set; }
    }
}

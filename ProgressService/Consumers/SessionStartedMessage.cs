namespace ProgressService.Consumers
{
    public class SessionStartedMessage
    {
        public Guid SessionId { get; set; }
        public Guid UserId { get; set; }
        public Guid WorkoutId { get; set; }
        public DateTime StartTime { get; set; }
        public string Status { get; set; }
    }
}

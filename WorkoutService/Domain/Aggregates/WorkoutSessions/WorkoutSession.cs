namespace WorkoutService.Domain.Aggregates.WorkoutSessions
{
    public class WorkoutSession
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid WorkoutId { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public string Status { get; private set; }

        private WorkoutSession() { }

        private WorkoutSession(Guid userId, Guid workoutId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            WorkoutId = workoutId;
            StartTime = DateTime.UtcNow;
            Status = WorkoutSessionStatus.Active;
        }

        public static WorkoutSession Start(Guid userId, Guid workoutId)
        {
            return new WorkoutSession(userId, workoutId);
        }

        public void CompleteSession()
        {
            if (Status != WorkoutSessionStatus.Active)
                throw new InvalidOperationException("Only active sessions can be completed.");

            EndTime = DateTime.UtcNow;
            Status = WorkoutSessionStatus.Completed;
        }
    }
}

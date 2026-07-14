using WorkoutService.Common.Exceptions;
using WorkoutService.Common.Responses;

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

        // For creating a stub/disconnected entity for partial updates
        public WorkoutSession(Guid id)
        {
            Id = id;
        }
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
                throw new DomainException(ErrorCode.WorkoutSessionNotActive);

            EndTime = DateTime.UtcNow;
            Status = WorkoutSessionStatus.Completed;
        }
    }
}

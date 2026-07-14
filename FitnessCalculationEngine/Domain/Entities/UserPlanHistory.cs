namespace FitnessCalculationEngine.Domain.Entities;

// Immutable / insert-only.
public class UserPlanHistory
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string PlanId { get; set; } = string.Empty;
    public DateTime AssignedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public string ReasonForChange { get; set; } = string.Empty;
}

namespace Elevate.Fce.Domain.Entities;

public class UserAssignedPlans
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string PlanId { get; set; } = string.Empty;
    public DateTime AssignedAt { get; set; }
    public bool IsActive { get; set; }

    public FitnessPlanConfig? Plan { get; set; }
}

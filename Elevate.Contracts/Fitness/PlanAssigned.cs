namespace Elevate.Contracts.Fitness;

// Published by FCE when a plan is assigned to a user.
public record PlanAssigned(
    Guid UserId,
    Guid PlanId,
    DateTime AssignedAtUtc);

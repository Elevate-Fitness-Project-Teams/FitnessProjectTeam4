namespace Elevate.Contracts.Fitness;

// Published by Progress service, consumed by FCE.
public record WeightUpdated(
    Guid UserId,
    decimal NewWeightKg,
    DateTime OccurredAtUtc);

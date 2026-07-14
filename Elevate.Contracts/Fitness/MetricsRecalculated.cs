namespace Elevate.Contracts.Fitness;

// Published by FCE after recalculating user metrics.
public record MetricsRecalculated(
    Guid UserId,
    DateTime CalculatedAtUtc);

namespace FitnessCalculationEngine.Common.Messaging.Events;

public record WeightUpdatedEvent(
    Guid UserId,
    decimal NewWeight,
    string? Reason
);

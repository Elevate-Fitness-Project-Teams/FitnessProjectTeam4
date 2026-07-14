namespace FitnessCalculationEngine.Features.Plans.DTOs;

public record AssignedPlanDto(
    string PlanId,
    string PlanName,
    string Description,
    string Goal,
    string Status,
    decimal MinCalorie,
    decimal MaxCalorie,
    string EstimatedDuration,
    int WorkoutsPerWeek,
    string ProgramType,
    DateTime AssignedAt
);

using FitnessCalculationEngine.Domain.Enums;

namespace FitnessCalculationEngine.Features.FitnessStats.DTOs;

public record SubmitStatsDto(
    decimal Weight,
    decimal Height,
    int Age,
    Gender Gender,
    Goal Goal,
    ActivityLevel ActivityLevel
);

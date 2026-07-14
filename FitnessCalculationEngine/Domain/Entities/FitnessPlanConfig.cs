using FitnessCalculationEngine.Domain.Enums;

namespace FitnessCalculationEngine.Domain.Entities;

// PK is a string (PlanId) — maps to the Workout service's plan keys.
public class FitnessPlanConfig
{
    public string PlanId { get; set; } = string.Empty;
    public string PlanName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Goal Goal { get; set; }
    public FitnessStatus Status { get; set; }
    public decimal MinCalorie { get; set; }
    public decimal MaxCalorie { get; set; }
    public string EstimatedDuration { get; set; } = string.Empty;
    public int WorkoutsPerWeek { get; set; }
    public string ProgramType { get; set; } = string.Empty;
}

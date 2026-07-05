using Elevate.Fce.Domain.Enums;

namespace Elevate.Fce.Domain.Entities;

// PK is a string (PlanId) — maps to the Workout service's plan keys.
public class FitnessPlanConfig
{
    public string PlanId { get; set; } = string.Empty;
    public string PlanName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Goal Goal { get; set; }
    public FitnessStatus Status { get; set; }
    public double MinCalorie { get; set; }
    public double MaxCalorie { get; set; }
    public string EstimatedDuration { get; set; } = string.Empty;
    public int WorkoutsPerWeek { get; set; }
    public string ProgramType { get; set; } = string.Empty;
}

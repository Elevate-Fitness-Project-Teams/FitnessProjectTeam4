using Elevate.Fce.Domain.Enums;

namespace Elevate.Fce.Domain.Entities;

// UserId comes from the JWT, never from the request body.
public class UserFitnessStats
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public Goal Goal { get; set; }
    public ActivityLevel ActivityLevel { get; set; }
    public DateTime RecordedAt { get; set; }
}

using FitnessCalculationEngine.Common.Persistence;
using FitnessCalculationEngine.Domain.Entities;
using MediatR;

namespace FitnessCalculationEngine.Features.FitnessStats.Commands.SubmitStats;

public class SubmitStatsHandler(IRepository repository) : IRequestHandler<SubmitStatsCommand, Guid>
{
    public async Task<Guid> Handle(SubmitStatsCommand cmd, CancellationToken ct)
    {
        var stats = new UserFitnessStats
        {
            UserId = cmd.UserId,
            Weight = cmd.Weight,
            Height = cmd.Height,
            Age = cmd.Age,
            Gender = cmd.Gender,
            Goal = cmd.Goal,
            ActivityLevel = cmd.ActivityLevel,
            RecordedAt = DateTime.UtcNow
        };

        repository.Add(stats);
        await repository.SaveChangesAsync(ct);

        return stats.Id;
    }
}

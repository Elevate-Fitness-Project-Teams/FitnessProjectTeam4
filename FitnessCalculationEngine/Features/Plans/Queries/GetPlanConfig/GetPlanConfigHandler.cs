using FitnessCalculationEngine.Common.Api;
using FitnessCalculationEngine.Common.Persistence;
using FitnessCalculationEngine.Domain.Entities;
using FitnessCalculationEngine.Features.Plans.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessCalculationEngine.Features.Plans.Queries.GetPlanConfig;

public class GetPlanConfigHandler(IRepository repository) : IRequestHandler<GetPlanConfigQuery, PlanConfigDto>
{
    public async Task<PlanConfigDto> Handle(GetPlanConfigQuery q, CancellationToken ct)
    {
        var dto = await repository.Query<FitnessPlanConfig>()
            .AsNoTracking()
            .Where(p => p.PlanId == q.PlanId)
            .Select(p => new PlanConfigDto(
                p.PlanId,
                p.PlanName,
                p.Description,
                p.Goal.ToString(),
                p.Status.ToString(),
                p.MinCalorie,
                p.MaxCalorie,
                p.EstimatedDuration,
                p.WorkoutsPerWeek,
                p.ProgramType))
            .FirstOrDefaultAsync(ct);

        return dto ?? throw new AppException(404, ErrorCodes.RES_PLAN_NOT_FOUND, "Plan not found.");
    }
}

using FitnessCalculationEngine.Common;
using FitnessCalculationEngine.Common.Persistence;
using FitnessCalculationEngine.Domain.Entities;
using FitnessCalculationEngine.Features.Plans.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FitnessCalculationEngine.Features.Plans.Queries.GetPlanConfigs;

public class GetPlanConfigsHandler(IRepository repository)
    : IRequestHandler<GetPlanConfigsQuery, PagedResult<PlanConfigDto>>
{
    public Task<PagedResult<PlanConfigDto>> Handle(GetPlanConfigsQuery q, CancellationToken ct)
    {
        var query = repository.Query<FitnessPlanConfig>().AsNoTracking();

        if (q.Goal is not null)
            query = query.Where(p => p.Goal == q.Goal);

        if (q.Status is not null)
            query = query.Where(p => p.Status == q.Status);

        var projected = query
            .OrderBy(p => p.Goal)
            .ThenBy(p => p.Status)
            .ThenBy(p => p.MinCalorie)
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
                p.ProgramType));

        return projected.ToPagedResultAsync(q.Page, q.PageSize, ct);
    }
}

using FitnessCalculationEngine.Common;
using FitnessCalculationEngine.Domain.Enums;
using FitnessCalculationEngine.Features.Plans.DTOs;
using MediatR;

namespace FitnessCalculationEngine.Features.Plans.Queries.GetPlanConfigs;

public record GetPlanConfigsQuery(
    Goal? Goal = null,
    FitnessStatus? Status = null,
    int Page = 1,
    int PageSize = 20) : IRequest<PagedResult<PlanConfigDto>>;

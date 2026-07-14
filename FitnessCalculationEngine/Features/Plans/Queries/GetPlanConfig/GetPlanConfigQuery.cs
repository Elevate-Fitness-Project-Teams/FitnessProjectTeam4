using FitnessCalculationEngine.Features.Plans.DTOs;
using MediatR;

namespace FitnessCalculationEngine.Features.Plans.Queries.GetPlanConfig;

public record GetPlanConfigQuery(string PlanId) : IRequest<PlanConfigDto>;

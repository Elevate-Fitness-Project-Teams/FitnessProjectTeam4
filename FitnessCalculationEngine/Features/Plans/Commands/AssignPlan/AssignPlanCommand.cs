using FitnessCalculationEngine.Features.Plans.DTOs;
using MediatR;

namespace FitnessCalculationEngine.Features.Plans.Commands.AssignPlan;

public record AssignPlanCommand(Guid UserId) : IRequest<AssignedPlanDto>;

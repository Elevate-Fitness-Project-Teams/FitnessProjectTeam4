using MediatR;
using ProgressService.Common.Responses;
using ProgressService.Features.Progress.Dtos;

namespace ProgressService.Features.Progress.Commands.LogWeight
{
    public record LogWeightCommand(
        string UserId,
        double Weight,
        DateTime Date,
        string? Notes
    ) : IRequest<RequestResult<LogWeightResponseDto>>;
}

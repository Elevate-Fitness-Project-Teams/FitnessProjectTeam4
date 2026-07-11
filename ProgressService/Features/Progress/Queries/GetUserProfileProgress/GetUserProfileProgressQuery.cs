using MediatR;
using ProgressService.Common.Responses;
using ProgressService.Features.Progress.Dtos;

namespace ProgressService.Features.Progress.Queries.GetUserProfileProgress
{
    public record GetUserProfileProgressQuery(string UserId) : IRequest<RequestResult<CombinedProgressProfileDto>>;
}

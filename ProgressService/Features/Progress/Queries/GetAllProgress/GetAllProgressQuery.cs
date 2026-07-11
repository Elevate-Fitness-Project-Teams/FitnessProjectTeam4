using MediatR;
using ProgressService.Common.Responses;
using ProgressService.Features.Progress.Dtos;

namespace ProgressService.Features.Progress.Queries.GetAllProgress
{
    public record GetAllProgressQuery() : IRequest<RequestResult<List<ProgressOverviewDto>>>;
}

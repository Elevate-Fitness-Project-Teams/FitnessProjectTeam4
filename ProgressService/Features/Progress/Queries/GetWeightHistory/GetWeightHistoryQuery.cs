using MediatR;
using ProgressService.Common.Responses;
using ProgressService.Features.Progress.Dtos;

namespace ProgressService.Features.Progress.Queries.GetWeightHistory
{
    public record GetWeightHistoryQuery(string UserId) : IRequest<RequestResult<List<WeightHistoryDto>>>;
}

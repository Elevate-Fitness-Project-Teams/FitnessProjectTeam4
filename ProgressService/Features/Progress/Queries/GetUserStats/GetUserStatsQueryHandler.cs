using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgressService.Common.Responses;
using ProgressService.Domian.Aggregates.UserStatistics;
using ProgressService.Features.Progress.Dtos;
using ProgressService.Infrastructure.Data.Repositories;

namespace ProgressService.Features.Progress.Queries.GetUserStats
{
    public class GetUserStatsQueryHandler(
        IGenericRepository<UserStatistic> _statsRepository,
        IMapper _mapper
        ) : IRequestHandler<GetUserStatsQuery, RequestResult<UserStatsDto>>
    {
        public async Task<RequestResult<UserStatsDto>> Handle(GetUserStatsQuery request, CancellationToken ct)
        {
            var stats = await _statsRepository.GetAll()
                .FirstOrDefaultAsync(s => s.UserId == request.UserId, ct);

            if (stats == null)
                return RequestResult<UserStatsDto>.Failure(ErrorCode.UserStatisticsNotFound);

            var dto = _mapper.Map<UserStatsDto>(stats);
            return RequestResult<UserStatsDto>.Success(dto);
        }
    }
}

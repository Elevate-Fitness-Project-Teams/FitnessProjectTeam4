using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgressService.Common.Responses;
using ProgressService.Domian.Aggregates.WorkoutLogs;
using ProgressService.Features.Progress.Dtos;
using ProgressService.Infrastructure.Data.Repositories;

namespace ProgressService.Features.Progress.Queries.GetAllProgress
{
    public class GetAllProgressQueryHandler(
       IGenericRepository<WorkoutLog> _logRepository,
       IMapper _mapper
       ) : IRequestHandler<GetAllProgressQuery, RequestResult<List<ProgressOverviewDto>>>
    {
        public async Task<RequestResult<List<ProgressOverviewDto>>> Handle(GetAllProgressQuery request, CancellationToken ct)
        {
            var logs = await _logRepository.GetAll()
                .OrderByDescending(l => l.CompletedAt)
                .Take(50) 
                .ToListAsync(ct);

            if (!logs.Any())
                return RequestResult<List<ProgressOverviewDto>>.Failure(ErrorCode.ProgressNotFound);

            var dtos = _mapper.Map<List<ProgressOverviewDto>>(logs);
            return RequestResult<List<ProgressOverviewDto>>.Success(dtos);
        }
    }
}

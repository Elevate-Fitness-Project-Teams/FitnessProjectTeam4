using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgressService.Common.Responses;
using ProgressService.Domian.Aggregates.WeightHistories;
using ProgressService.Features.Progress.Dtos;
using ProgressService.Infrastructure.Data.Repositories;

namespace ProgressService.Features.Progress.Queries.GetWeightHistory
{
    public class GetWeightHistoryQueryHandler(
        IGenericRepository<WeightHistory> _weightRepository,
        IMapper _mapper
        ) : IRequestHandler<GetWeightHistoryQuery, RequestResult<List<WeightHistoryDto>>>
    {
        public async Task<RequestResult<List<WeightHistoryDto>>> Handle(GetWeightHistoryQuery request, CancellationToken ct)
        {
            var history = await _weightRepository.GetAll()
                .Where(w => w.UserId == request.UserId)
                .OrderByDescending(w => w.Date)
                .ToListAsync(ct);

            if (history == null)
                return RequestResult<List<WeightHistoryDto>>.Failure(ErrorCode.WeightHistoryNotFound);

            var dtos = _mapper.Map<List<WeightHistoryDto>>(history);
            return RequestResult<List<WeightHistoryDto>>.Success(dtos);
        }
    }
}

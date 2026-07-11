using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProgressService.Common.Responses;
using ProgressService.Domian.Aggregates.UserStatistics;
using ProgressService.Domian.Aggregates.WeightHistories;
using ProgressService.Features.Progress.Commands.CreateUserStatistic;
using ProgressService.Features.Progress.Common.Queries.GetUserStatistics;
using ProgressService.Features.Progress.Dtos;
using ProgressService.Infrastructure.Data.Repositories;
using SharedMessages.Messages;
using SharedMessages.Queues;

namespace ProgressService.Features.Progress.Commands.LogWeight
{
    public class LogWeightOrchestratorHandler(
        IGenericRepository<WeightHistory> _weightRepository,
        IUnitOfWork _unitOfWork,
        IMediator _mediator,
        ILogger<LogWeightOrchestratorHandler> _logger,
        ISendEndpointProvider _sendEndpoint
        ) : IRequestHandler<LogWeightCommand, RequestResult<LogWeightResponseDto>>
    {
        public async Task<RequestResult<LogWeightResponseDto>> Handle(LogWeightCommand request, CancellationToken ct)
        {
            _logger.LogInformation("Starting weight logging for UserId: {UserId}", request.UserId);
            try
            {
                double previousWeight = 0;

                var resultDto = await _unitOfWork.ExecuteAsync(async () =>
                {
                    var lastEntry = await _weightRepository.GetAll()
                        .Where(w => w.UserId == request.UserId)
                        .OrderByDescending(w => w.Date)
                        .FirstOrDefaultAsync(ct);
                    _logger.LogInformation("Previous weight retrieved for UserId: {UserId}. PreviousWeight: {PreviousWeight}", request.UserId, previousWeight);

                    previousWeight = lastEntry?.Weight ?? request.Weight;

                    var weightHistory = new WeightHistory(Guid.NewGuid(), request.UserId, request.Weight, request.Date, request.Notes);
                    await _weightRepository.AddAsync(weightHistory, ct);
                    _logger.LogInformation("Weight history added. UserId: {UserId}, Weight: {Weight}, Date: {Date}", request.UserId, request.Weight, request.Date);

                    var stats = await _mediator.Send(new GetUserStatisticsQuery(request.UserId), ct);
                    if (stats == null)
                    {
                        _logger.LogInformation("No user statistics found. Creating initial statistics for UserId: {UserId}", request.UserId);
                        stats = new UserStatistic(Guid.NewGuid(), request.UserId, request.Weight);
                        await _mediator.Send(new CreateUserStatisticCommand(stats), ct); //Adding New Stat
                    }
                    else
                    {
                        stats.UpdateWeightMetrics(request.Weight);
                        _logger.LogInformation("User statistics updated for UserId: {UserId}. CurrentWeight: {CurrentWeight}, TotalWeightLost: {WeightLost}", request.UserId, stats.CurrentWeight, stats.TotalWeightLost);
                    }

                    double diffFromPrevious = request.Weight - previousWeight;


                    double bmi = (request.Weight / (1.70 * 1.70));

                    _logger.LogInformation("Publishing WeightUpdatedIntegrationMessage for UserId: {UserId}", request.UserId);
                    // Publish Event
                    var endPoint = await _sendEndpoint.GetSendEndpoint(new Uri($"queue:{QueueNames.WeightUpdated}"));
                    var message = new WeightUpdatedIntegrationMessage(
                        request.UserId,
                        request.Weight,
                        request.Date);

                    await endPoint.Send(message);
                    _logger.LogInformation("WeightUpdatedIntegrationMessage published successfully for UserId: {UserId}", request.UserId);

                    return new LogWeightResponseDto(bmi, diffFromPrevious, stats.TotalWeightLost);
                }, ct);

                _logger.LogInformation("Weight logged successfully for UserId: {UserId}", request.UserId);
                return RequestResult<LogWeightResponseDto>.Success(resultDto);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _logger.LogWarning(ex, "Validation error while logging weight for UserId: {UserId}", request.UserId);
                return RequestResult<LogWeightResponseDto>.Failure(ErrorCode.ValidationError, ex.Message);
            }
        }
    }
    
}

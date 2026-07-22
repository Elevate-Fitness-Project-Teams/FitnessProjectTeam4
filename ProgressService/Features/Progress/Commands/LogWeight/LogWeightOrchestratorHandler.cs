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
            _logger.LogInformation(
                "Starting weight logging for UserId: {UserId}",
                request.UserId);

            try
            {
                double previousWeight = 0;

                var resultDto = await _unitOfWork.ExecuteAsync(async () =>
                {
                    var lastEntry = await _weightRepository.GetAll()
                        .Where(w => w.UserId == request.UserId)
                        .OrderByDescending(w => w.Date)
                        .FirstOrDefaultAsync(ct);

                    previousWeight = lastEntry?.Weight ?? request.Weight;

                    _logger.LogInformation(
                        "Previous weight retrieved for UserId: {UserId}. PreviousWeight: {PreviousWeight}",
                        request.UserId,
                        previousWeight);

                    var weightHistory = new WeightHistory(
                        Guid.NewGuid(),
                        request.UserId,
                        request.Weight,
                        request.Date,
                        request.Notes
                    );

                    await _weightRepository.AddAsync(weightHistory, ct);

                    _logger.LogInformation(
                        "Weight history added. UserId: {UserId}, Weight: {Weight}, Date: {Date}",
                        request.UserId,
                        request.Weight,
                        request.Date);

                    var stats = await _mediator.Send(new GetUserStatisticsQuery(request.UserId), ct);

                    if (stats == null)
                    {
                        _logger.LogInformation(
                            "No user statistics found. Creating initial statistics for UserId: {UserId}",
                            request.UserId);

                        stats = new UserStatistic(Guid.NewGuid(), request.UserId, request.Weight);
                        await _mediator.Send(new CreateUserStatisticCommand(stats), ct);
                    }
                    else
                    {
                        stats.UpdateWeightMetrics(request.Weight);

                        _logger.LogInformation(
                            "User statistics updated for UserId: {UserId}. CurrentWeight: {CurrentWeight}, TotalWeightLost: {WeightLost}",
                            request.UserId,
                            stats.CurrentWeight,
                            stats.TotalWeightLost);
                    }

                    double diffFromPrevious = request.Weight - previousWeight;
                    double bmi = request.Weight / (1.70 * 1.70);

                    var endPoint = await _sendEndpoint.GetSendEndpoint(
                        new Uri($"queue:{QueueNames.WeightUpdated}"));

                    await endPoint.Send(new WeightUpdatedIntegrationMessage(
                        request.UserId,
                        request.Weight,
                        request.Date));

                    _logger.LogInformation(
                        "WeightUpdatedIntegrationMessage published successfully for UserId: {UserId}",
                        request.UserId);

                    return new LogWeightResponseDto(bmi, diffFromPrevious, stats.TotalWeightLost);
                }, ct);

                _logger.LogInformation(
                    "Weight logged successfully for UserId: {UserId}",
                    request.UserId);

                return RequestResult<LogWeightResponseDto>.Success(resultDto);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Validation error while logging weight for UserId: {UserId}",
                    request.UserId);

                return RequestResult<LogWeightResponseDto>.Failure(ErrorCode.ValidationError, ex.Message);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Concurrent update detected while updating weight statistics for UserId: {UserId}",
                    request.UserId);

                return RequestResult<LogWeightResponseDto>.Failure(
                    ErrorCode.ConcurrencyConflict,
                    "The user's statistics were modified by another request. Please try again.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Database constraint violation while logging weight for UserId: {UserId}",
                    request.UserId);

                return RequestResult<LogWeightResponseDto>.Failure(
                    ErrorCode.ValidationError,
                    "A database constraint was violated while saving the weight record.");
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unexpected error while logging weight for UserId: {UserId}",
                    request.UserId);

                return RequestResult<LogWeightResponseDto>.Failure(
                    ErrorCode.InternalServerError,
                    "An unexpected error occurred while logging the weight.");
            }
        }
    }
    
}

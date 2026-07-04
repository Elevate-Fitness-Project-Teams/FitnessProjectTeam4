using AutoMapper;
using MediatR;
using ProfileService.Common.GenericResult;
using ProfileService.Features.UpdateSettings.Commands;
using ProfileService.Features.UpdateSettings.DTOs;

namespace ProfileService.Features.UpdateSettings.Orchestrator.Handlers
{
    public class UpdateSettingsOrchestratorHandler : IRequestHandler<UpdateSettingsOrchestrator, ResponseViewModel<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSettingsOrchestratorHandler> _logger;

        public UpdateSettingsOrchestratorHandler(
            IMediator mediator,
            IMapper mapper,
            ILogger<UpdateSettingsOrchestratorHandler> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResponseViewModel<bool>> Handle(UpdateSettingsOrchestrator request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Orchestrating UpdateSettings for UserId: {UserId}", request.UserId);

            var settingsDto = _mapper.Map<UpdateSettingsDto>(request.RequestViewModel);

            var commandResult = await _mediator.Send(new UpdateSettingsCommand(request.UserId, settingsDto), cancellationToken);

            if (!commandResult.isSuccess)
            {
                return ResponseViewModel<bool>.Failure(
                    commandResult.message ?? "Update failed",
                    commandResult.errorCode);
            }

            _logger.LogInformation("Successfully orchestrated UpdateSettings for UserId: {UserId}", request.UserId);

            return ResponseViewModel<bool>.Success(true, "Settings updated successfully.");
        }
    }
}

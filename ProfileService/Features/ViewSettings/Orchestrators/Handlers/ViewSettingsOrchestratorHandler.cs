using AutoMapper;
using MediatR;
using ProfileService.Common.Enum;
using ProfileService.Common.GenericResult;
using ProfileService.Common.Services.Interfaces;
using ProfileService.Features.ViewSettings.Queries;
using ProfileService.Features.ViewSettings.ViewModels;

namespace ProfileService.Features.ViewSettings.Orchestrators.Handlers
{
    public class ViewSettingsOrchestratorHandler : IRequestHandler<ViewSettingsOrchestrator, ResponseViewModel<ViewSettingsResponseViewModel>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<ViewSettingsOrchestratorHandler> _logger;

        public ViewSettingsOrchestratorHandler(
            IMediator mediator,
            IMapper mapper,
            ILogger<ViewSettingsOrchestratorHandler> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResponseViewModel<ViewSettingsResponseViewModel>> Handle(ViewSettingsOrchestrator request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Orchestrating ViewSettings for UserId: {UserId}", request.UserId);

            var queryResult = await _mediator.Send(new GetUserSettingsQuery(request.UserId), cancellationToken);

            if (!queryResult.isSuccess)
            {
                return ResponseViewModel<ViewSettingsResponseViewModel>.Failure(
                    queryResult.message ?? "Failed to retrieve settings",
                    queryResult.errorCode);
            }

            var responseData = _mapper.Map<ViewSettingsResponseViewModel>(queryResult.data);
            return ResponseViewModel<ViewSettingsResponseViewModel>.Success(responseData, "Settings retrieved successfully.");
        }
    }
}

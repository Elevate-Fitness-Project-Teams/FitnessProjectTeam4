using AutoMapper;
using MediatR;
using ProfileService.Common.GenericResult;
using ProfileService.Features.ViewProfile.Queries;
using ProfileService.Features.ViewProfile.ViewModels;

namespace ProfileService.Features.ViewProfile.Orchestrators.Handler
{
    public class ViewProfileOrchestratorHandler : IRequestHandler<ViewProfileOrchestrator, RequestResult<ProfileViewModel>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<ViewProfileOrchestratorHandler> _logger;

        public ViewProfileOrchestratorHandler(
            IMediator mediator,
            IMapper mapper,
            ILogger<ViewProfileOrchestratorHandler> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RequestResult<ProfileViewModel>> Handle(ViewProfileOrchestrator request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Orchestrating ViewProfile for UserId: {UserId}", request.UserId);

            var profileResult = await _mediator.Send(new GetProfileDataQuery(request.UserId), cancellationToken);

            if (!profileResult.isSuccess || profileResult.data == null)
            {
                _logger.LogWarning("ViewProfile Orchestration stopped: {Message}", profileResult.message);
                return RequestResult<ProfileViewModel>.Failure(profileResult.message, profileResult.errorCode);
            }

            var statisticsResult = await _mediator.Send(new GetProfileStatisticsQuery(request.UserId), cancellationToken);

            var viewModel = _mapper.Map<ProfileViewModel>(profileResult.data);

            if (statisticsResult.isSuccess && statisticsResult.data != null)
            {
                _mapper.Map(statisticsResult.data, viewModel);
            }

            _logger.LogInformation("Successfully orchestrated ViewProfile for UserId: {UserId}", request.UserId);

            return RequestResult<ProfileViewModel>.Success(viewModel);
        }
    }
}

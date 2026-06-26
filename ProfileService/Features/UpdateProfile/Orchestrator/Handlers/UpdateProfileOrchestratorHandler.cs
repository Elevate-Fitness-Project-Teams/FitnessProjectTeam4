using MediatR;
using ProfileService.Common.Enum;
using ProfileService.Common.GenericResult;
using ProfileService.Common.Services.Interfaces;
using ProfileService.Features.UpdateProfile.Commands;
using ProfileService.Features.UpdateProfile.Queries;

namespace ProfileService.Features.UpdateProfile.Orchestrator.Handlers
{
    public class UpdateProfileOrchestratorHandler : IRequestHandler<UpdateProfileOrchestrator, ResponseViewModel<bool>>
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<UpdateProfileOrchestratorHandler> _logger;

        public UpdateProfileOrchestratorHandler(
            IMediator mediator,
            ICurrentUserService currentUserService,
            ILogger<UpdateProfileOrchestratorHandler> logger)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<ResponseViewModel<bool>> Handle(UpdateProfileOrchestrator request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                _logger.LogWarning("UpdateProfile Orchestration failed. Unauthorized access attempt (UserId is null).");
                return ResponseViewModel<bool>.Failure("AUTH_TOKEN_INVALID", ErrorCode.AuthTokenInvalid);
            }

            _logger.LogInformation("Orchestrating UpdateProfile for UserId: {UserId}", userId);

            var emailCheckResult = await _mediator.Send(new GetUserEmailForCheckQuery(userId.Value), cancellationToken);

            if (!emailCheckResult.isSuccess)
            {
                _logger.LogWarning("Validation failed: User {UserId} not found in DB.", userId);
                return ResponseViewModel<bool>.Failure(emailCheckResult.message ?? "Profile not found", emailCheckResult.errorCode);
            }

            _logger.LogInformation("User exists (Email: {Email}). Proceeding with update.", emailCheckResult.data);

            var command = new UpdateProfileCommand(
                userId.Value,
                request.RequestViewModel.FirstName,
                request.RequestViewModel.LastName,
                request.RequestViewModel.PhoneNumber
            );

            var commandResult = await _mediator.Send(command, cancellationToken);

            if (!commandResult.isSuccess)
            {
                _logger.LogWarning("UpdateProfile Orchestration failed for UserId: {UserId}. Reason: {Message}", userId, commandResult.message);
                return ResponseViewModel<bool>.Failure(commandResult.message ?? "Update failed", commandResult.errorCode);
            }

            _logger.LogInformation("Successfully orchestrated UpdateProfile for UserId: {UserId}", userId);

            return ResponseViewModel<bool>.Success(true, "Profile updated successfully.");
        }
    }
}

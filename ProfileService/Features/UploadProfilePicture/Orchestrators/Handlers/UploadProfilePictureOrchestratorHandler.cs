using AutoMapper;
using MediatR;
using ProfileService.Common.Enum;
using ProfileService.Common.GenericResult;
using ProfileService.Common.Services.Interfaces;
using ProfileService.Features.UploadProfilePicture.Commands;
using ProfileService.Features.UploadProfilePicture.DTOs;
using ProfileService.Features.UploadProfilePicture.Queries;
using ProfileService.Features.UploadProfilePicture.ViewModels;

namespace ProfileService.Features.UploadProfilePicture.Orchestrators.Handlers
{
    public class UploadProfilePictureOrchestratorHandler : IRequestHandler<UploadProfilePictureOrchestrator, ResponseViewModel<UploadProfilePictureResponseViewModel>>
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly ILogger<UploadProfilePictureOrchestratorHandler> _logger;

        public UploadProfilePictureOrchestratorHandler(
            IMediator mediator,
            ICurrentUserService currentUserService,
            IMapper mapper,
            ILogger<UploadProfilePictureOrchestratorHandler> logger)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ResponseViewModel<UploadProfilePictureResponseViewModel>> Handle(UploadProfilePictureOrchestrator request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                _logger.LogWarning("UploadProfilePicture Orchestration failed. Unauthorized access attempt (UserId is null).");
                return ResponseViewModel<UploadProfilePictureResponseViewModel>.Failure("AUTH_TOKEN_INVALID", ErrorCode.AuthTokenInvalid);
            }

            _logger.LogInformation("Orchestrating UploadProfilePicture for UserId: {UserId}", userId);

            var existsResult = await _mediator.Send(new CheckProfileExistsForUploadQuery(userId.Value), cancellationToken);

            if (!existsResult.isSuccess)
            {
                _logger.LogWarning("Validation failed: User {UserId} not found in DB.", userId);
                return ResponseViewModel<UploadProfilePictureResponseViewModel>.Failure(existsResult.message ?? "Profile not found", existsResult.errorCode);
            }

            var file = request.RequestViewModel.File;

            using var stream = file.OpenReadStream();

            var fileDto = new UploadProfilePictureDto
            {
                FileName = file.FileName,
                ContentType = file.ContentType,
                Content = stream
            };

            var command = new UploadProfilePictureCommand(userId.Value, fileDto);
            var commandResult = await _mediator.Send(command, cancellationToken);

            if (!commandResult.isSuccess)
            {
                _logger.LogWarning("UploadProfilePicture Command failed for UserId: {UserId}. Reason: {Message}", userId, commandResult.message);
                return ResponseViewModel<UploadProfilePictureResponseViewModel>.Failure(commandResult.message ?? "Upload failed", commandResult.errorCode);
            }

            _logger.LogInformation("Successfully orchestrated UploadProfilePicture for UserId: {UserId}", userId);

            var responseData = _mapper.Map<UploadProfilePictureResponseViewModel>(commandResult.data);

            return ResponseViewModel<UploadProfilePictureResponseViewModel>.Success(responseData, "Profile picture uploaded successfully.");
        }
    }
}

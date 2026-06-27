using MediatR;
using ProfileService.Common.Enum;
using ProfileService.Common.GenericResult;
using ProfileService.Domain.Entities;
using ProfileService.Infrastructure.Repository;

namespace ProfileService.Features.UploadProfilePicture.Queries.Handlers
{
    public class CheckProfileExistsForUploadQueryHandler : IRequestHandler<CheckProfileExistsForUploadQuery, RequestResult<bool>>
    {
        private readonly IGenericRepository<UserProfile> _profileRepository;
        private readonly ILogger<CheckProfileExistsForUploadQueryHandler> _logger;

        public CheckProfileExistsForUploadQueryHandler(
            IGenericRepository<UserProfile> profileRepository,
            ILogger<CheckProfileExistsForUploadQueryHandler> logger)
        {
            _profileRepository = profileRepository;
            _logger = logger;
        }

        public async Task<RequestResult<bool>> Handle(CheckProfileExistsForUploadQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Checking if profile exists for upload. UserId: {UserId}", request.UserId);

            var exists = await _profileRepository.AnyAsync(x => x.Id == request.UserId, cancellationToken);

            if (!exists)
            {
                _logger.LogWarning("Upload aborted. Profile not found for UserId: {UserId}", request.UserId);
                return RequestResult<bool>.Failure("Profile not found.", ErrorCode.ProfileNotFound);
            }

            _logger.LogInformation("Profile exists for UserId: {UserId}. Proceeding with upload.", request.UserId);
            return RequestResult<bool>.Success(true);
        }
    }
}

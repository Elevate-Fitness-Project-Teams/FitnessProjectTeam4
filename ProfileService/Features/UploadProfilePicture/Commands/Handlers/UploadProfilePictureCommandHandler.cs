using MediatR;
using ProfileService.Common.Enum;
using ProfileService.Common.GenericResult;
using ProfileService.Common.Services.Interfaces;
using ProfileService.Domain.Entities;
using ProfileService.Infrastructure.Repository;

namespace ProfileService.Features.UploadProfilePicture.Commands.Handlers
{
    public class UploadProfilePictureCommandHandler : IRequestHandler<UploadProfilePictureCommand, RequestResult<string>>
    {
        private readonly IGenericRepository<UserProfile> _profileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;
        private readonly ILogger<UploadProfilePictureCommandHandler> _logger;

        public UploadProfilePictureCommandHandler(
            IGenericRepository<UserProfile> profileRepository,
            IUnitOfWork unitOfWork,
            IFileStorageService fileStorageService,
            ILogger<UploadProfilePictureCommandHandler> logger)
        {
            _profileRepository = profileRepository;
            _unitOfWork = unitOfWork;
            _fileStorageService = fileStorageService;
            _logger = logger;
        }

        public async Task<RequestResult<string>> Handle(UploadProfilePictureCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting profile picture upload process for UserId: {UserId}", request.UserId);

            var profile = await _profileRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (profile == null)
            {
                _logger.LogWarning("Upload failed. Profile not found for UserId: {UserId}", request.UserId);
                return RequestResult<string>.Failure("Profile not found.", ErrorCode.ProfileNotFound);
            }

            var oldPictureUrl = profile.ProfilePictureUrl;

            string newPictureUrl;

            try
            {
                newPictureUrl = await _fileStorageService.UploadFileAsync(
                    request.FileDto.Content,
                    request.FileDto.FileName,
                    request.FileDto.ContentType,
                    cancellationToken);

                _logger.LogInformation("File uploaded successfully. New URL: {Url}", newPictureUrl);

                if (!string.IsNullOrWhiteSpace(oldPictureUrl))
                {
                    await _fileStorageService.DeleteFileAsync(oldPictureUrl);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while uploading/deleting the file for UserId: {UserId}", request.UserId);
                return RequestResult<string>.Failure("Failed to process image storage operations.", ErrorCode.BadRequest);
            }

            await _unitOfWork.ExecuteAsync(() =>
            {
                profile.ProfilePictureUrl = newPictureUrl;
                _profileRepository.PartialUpdate(profile, nameof(UserProfile.ProfilePictureUrl));
                return Task.CompletedTask;
            });

            _logger.LogInformation("Profile picture URL updated in database for UserId: {UserId}", request.UserId);

            return RequestResult<string>.Success(newPictureUrl);
        }
    }
}

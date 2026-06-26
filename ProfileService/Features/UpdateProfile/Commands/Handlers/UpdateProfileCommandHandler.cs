using AutoMapper;
using MediatR;
using ProfileService.Common.Enum;
using ProfileService.Common.GenericResult;
using ProfileService.Domain.Entities;
using ProfileService.Infrastructure.Repository;

namespace ProfileService.Features.UpdateProfile.Commands.Handlers
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, RequestResult<bool>>
    {
        private readonly IGenericRepository<UserProfile> _profileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateProfileCommandHandler> _logger;

        public UpdateProfileCommandHandler(
            IGenericRepository<UserProfile> profileRepository,
            IUnitOfWork unitOfWork,
            ILogger<UpdateProfileCommandHandler> logger)
        {
            _profileRepository = profileRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<RequestResult<bool>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting profile update for UserId: {UserId}", request.UserId);

            var profile = await _profileRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (profile == null)
            {
                _logger.LogWarning("Profile update failed. Profile not found for UserId: {UserId}", request.UserId);
                return RequestResult<bool>.Failure("Profile not found.", ErrorCode.ProfileNotFound);
            }

            await _unitOfWork.ExecuteAsync(() =>
            {
                var updatedProperties = new List<string>();

                if (!string.IsNullOrWhiteSpace(request.FirstName))
                {
                    profile.FirstName = request.FirstName;
                    updatedProperties.Add(nameof(UserProfile.FirstName));
                }

                if (!string.IsNullOrWhiteSpace(request.LastName))
                {
                    profile.LastName = request.LastName;
                    updatedProperties.Add(nameof(UserProfile.LastName));
                }

                if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
                {
                    profile.PhoneNumber = request.PhoneNumber;
                    updatedProperties.Add(nameof(UserProfile.PhoneNumber));
                }

                if (updatedProperties.Any())
                {
                    _logger.LogInformation("Updating properties [{Properties}] for UserId: {UserId}", string.Join(", ", updatedProperties), request.UserId);
                    _profileRepository.PartialUpdate(profile, updatedProperties.ToArray());
                }
                else
                {
                    _logger.LogInformation("No valid properties provided to update for UserId: {UserId}", request.UserId);
                }

                return Task.CompletedTask;
            });

            _logger.LogInformation("Profile updated successfully for UserId: {UserId}", request.UserId);
            return RequestResult<bool>.Success(true);
        }
    }
}

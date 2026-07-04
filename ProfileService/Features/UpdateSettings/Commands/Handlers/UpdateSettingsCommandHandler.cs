using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileService.Common.Enum;
using ProfileService.Common.GenericResult;
using ProfileService.Domain.Entities;
using ProfileService.Features.UpdateSettings.DTOs;
using ProfileService.Infrastructure.Repository;

namespace ProfileService.Features.UpdateSettings.Commands.Handlers
{
    public class UpdateSettingsCommandHandler : IRequestHandler<UpdateSettingsCommand, RequestResult<bool>>
    {
        private readonly IGenericRepository<UserProfile> _profileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSettingsCommandHandler> _logger;

        public UpdateSettingsCommandHandler(
            IGenericRepository<UserProfile> profileRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<UpdateSettingsCommandHandler> logger)
        {
            _profileRepository = profileRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RequestResult<bool>> Handle(UpdateSettingsCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to patch settings for UserId: {UserId}", request.UserId);

            var profile = await GetTrackedProfileWithSettingsAsync(request.UserId, cancellationToken);

            if (profile == null)
            {
                _logger.LogWarning("Update settings failed. Profile not found for UserId: {UserId}", request.UserId);
                return RequestResult<bool>.Failure("Profile not found.", ErrorCode.ProfileNotFound);
            }

            ApplyPatch(profile, request.Dto);

            // 3. الحفظ (الـ EF Core Change Tracker هيكتشف التعديلات لوحده)
            await _unitOfWork.ExecuteAsync(() => Task.CompletedTask);

            _logger.LogInformation("Successfully patched settings for UserId: {UserId}", request.UserId);

            return RequestResult<bool>.Success(true);
        }


        private async Task<UserProfile?> GetTrackedProfileWithSettingsAsync(Guid userId, CancellationToken cancellationToken)
        {
            var query = _profileRepository.GetAsync(
                Filters: x => x.Id == userId,
                includes: source => source
                    .Include(p => p.Preferences)
                    .Include(p => p.NotificationSettings)
                    .Include(p => p.PrivacySettings),
                asNoTracking: false
            );

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        private void ApplyPatch(UserProfile profile, UpdateSettingsDto dto)
        {
            if (dto.Preferences != null)
            {
                profile.Preferences ??= new UserPreferences();
                _mapper.Map(dto.Preferences, profile.Preferences);
            }

            if (dto.Notifications != null)
            {
                profile.NotificationSettings ??= new NotificationSettings();
                _mapper.Map(dto.Notifications, profile.NotificationSettings);
            }

            if (dto.Privacy != null)
            {
                profile.PrivacySettings ??= new PrivacySettings();
                _mapper.Map(dto.Privacy, profile.PrivacySettings);
            }
        }
    }
}

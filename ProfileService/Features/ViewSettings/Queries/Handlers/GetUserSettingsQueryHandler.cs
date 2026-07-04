using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileService.Common.Enum;
using ProfileService.Common.GenericResult;
using ProfileService.Domain.Entities;
using ProfileService.Features.ViewSettings.DTOs;
using ProfileService.Infrastructure.Repository;

namespace ProfileService.Features.ViewSettings.Queries.Handlers
{
    public class GetUserSettingsQueryHandler : IRequestHandler<GetUserSettingsQuery, RequestResult<UserSettingsDto>>
    {
        private readonly IGenericRepository<UserProfile> _profileRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserSettingsQueryHandler> _logger;

        public GetUserSettingsQueryHandler(
            IGenericRepository<UserProfile> profileRepository,
            IMapper mapper,
            ILogger<GetUserSettingsQueryHandler> logger)
        {
            _profileRepository = profileRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RequestResult<UserSettingsDto>> Handle(GetUserSettingsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving settings for UserId: {UserId}", request.UserId);

            // جلب المستخدم مع كل الإعدادات المرتبطة به
            var query = _profileRepository.GetAsync(
                Filters: x => x.Id == request.UserId,
                includes: source => source
                    .Include(p => p.Preferences)
                    .Include(p => p.NotificationSettings)
                    .Include(p => p.PrivacySettings),
                asNoTracking: true
            );

            var profile = await query.FirstOrDefaultAsync(cancellationToken);

            if (profile == null)
            {
                _logger.LogWarning("Failed to retrieve settings. Profile not found for UserId: {UserId}", request.UserId);
                return RequestResult<UserSettingsDto>.Failure("Profile not found.", ErrorCode.ProfileNotFound);
            }

            var settingsDto = new UserSettingsDto
            {
                Preferences = _mapper.Map<UserPreferencesDto>(profile.Preferences ?? new UserPreferences()),
                Notifications = _mapper.Map<NotificationSettingsDto>(profile.NotificationSettings ?? new NotificationSettings()),
                Privacy = _mapper.Map<PrivacySettingsDto>(profile.PrivacySettings ?? new PrivacySettings())
            };

            _logger.LogInformation("Successfully retrieved settings for UserId: {UserId}", request.UserId);
            return RequestResult<UserSettingsDto>.Success(settingsDto);
        }
    }
}

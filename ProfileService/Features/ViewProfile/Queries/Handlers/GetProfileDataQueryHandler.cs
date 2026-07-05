using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileService.Common.Enum;
using ProfileService.Common.GenericResult;
using ProfileService.Domain.Entities;
using ProfileService.Features.ViewProfile.DTOs;
using ProfileService.Infrastructure.Data;
using ProfileService.Infrastructure.Repository;

namespace ProfileService.Features.ViewProfile.Queries.Handler
{
    public class GetProfileDataHandler : IRequestHandler<GetProfileDataQuery, RequestResult<ProfileDataDto>>
    {
        private readonly IGenericRepository<UserProfile> _userProfileRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProfileDataHandler> _logger;

        public GetProfileDataHandler(
            IGenericRepository<UserProfile> userProfileRepository,
            IMapper mapper,
            ILogger<GetProfileDataHandler> logger)
        {
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<RequestResult<ProfileDataDto>> Handle(GetProfileDataQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching profile data for user ID: {UserId}", request.UserId);

            var profile = await _userProfileRepository
                .GetAsync(p => p.Id == request.UserId, asNoTracking: true)
                .FirstOrDefaultAsync(cancellationToken);

            if (profile == null)
            {
                _logger.LogWarning("Profile not found for user ID: {UserId}", request.UserId);
                return RequestResult<ProfileDataDto>.Failure("Profile not found.", ErrorCode.ProfileNotFound);
            }

            var dto = _mapper.Map<ProfileDataDto>(profile);
            return RequestResult<ProfileDataDto>.Success(dto);
        }
    }
}

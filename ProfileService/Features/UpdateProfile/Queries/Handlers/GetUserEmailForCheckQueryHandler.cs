using MediatR;
using Microsoft.EntityFrameworkCore;
using ProfileService.Common.Enum;
using ProfileService.Common.GenericResult;
using ProfileService.Domain.Entities;
using ProfileService.Infrastructure.Repository;

namespace ProfileService.Features.UpdateProfile.Queries.Handlers
{
    public class GetUserEmailForCheckQueryHandler : IRequestHandler<GetUserEmailForCheckQuery, RequestResult<string>>
    {
        private readonly IGenericRepository<UserProfile> _profileRepository;
        private readonly ILogger<GetUserEmailForCheckQueryHandler> _logger;

        public GetUserEmailForCheckQueryHandler(
            IGenericRepository<UserProfile> profileRepository,
            ILogger<GetUserEmailForCheckQueryHandler> logger)
        {
            _profileRepository = profileRepository;
            _logger = logger;
        }

        public async Task<RequestResult<string>> Handle(GetUserEmailForCheckQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving email for UserId: {UserId}", request.UserId);

            var query = _profileRepository.GetAsync(x => x.Id == request.UserId);
            var profile = await query.FirstOrDefaultAsync(cancellationToken);

            if (profile == null)
            {
                _logger.LogWarning("Email retrieval failed. Profile not found for UserId: {UserId}", request.UserId);
                return RequestResult<string>.Failure("Profile not found.", ErrorCode.ProfileNotFound);
            }

            _logger.LogInformation("Successfully retrieved email for UserId: {UserId}", request.UserId);
            return RequestResult<string>.Success(profile.Email);
        }
    }
}

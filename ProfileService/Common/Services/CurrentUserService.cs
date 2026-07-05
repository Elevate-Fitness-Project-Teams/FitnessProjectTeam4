using ProfileService.Common.Services.Interfaces;
using System.Security.Claims;

namespace ProfileService.Common.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId
        {
            get
            {
                var userIdValue = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? User?.FindFirst("sub")?.Value;

                return Guid.TryParse(userIdValue, out var userId) ? userId : null;
            }
        }

        public string? Email => User?.FindFirst(ClaimTypes.Email)?.Value;

        public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

        public IReadOnlyList<string> Roles =>
            User?.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList() ?? [];

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
    }
}

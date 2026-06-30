using ProfileService.Common.Services.Interfaces;

namespace ProfileService.Common.Services
{
    public class MockCurrentUserService : ICurrentUserService
    {
        public Guid? UserId => Guid.Parse("11111111-1111-1111-1111-111111111111");

        public string? Email => "test@developer.com";
        public bool IsAuthenticated => true;
        public IReadOnlyList<string> Roles => new List<string> { "User" };
    }
}

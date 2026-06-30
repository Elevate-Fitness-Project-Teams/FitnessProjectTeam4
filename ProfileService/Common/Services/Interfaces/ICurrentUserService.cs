namespace ProfileService.Common.Services.Interfaces
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? Email { get; }
        bool IsAuthenticated { get; }
        IReadOnlyList<string> Roles { get; }
    }
}

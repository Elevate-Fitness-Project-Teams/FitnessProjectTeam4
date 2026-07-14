using System.Security.Claims;

namespace AuthService.Common.Api;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var raw = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                  ?? user.FindFirst("sub")?.Value;
        return Guid.TryParse(raw, out var id)
            ? id
            : throw new AppException(401, ErrorCodes.AUTH_TOKEN_INVALID, "Invalid token subject.");
    }
}

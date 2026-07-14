using System.Security.Claims;

namespace FitnessCalculationEngine.Common.Api;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var value = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(value, out var userId))
            throw new AppException(401, ErrorCodes.AUTH_TOKEN_INVALID, "User identity claim is missing or invalid.");
        return userId;
    }
}

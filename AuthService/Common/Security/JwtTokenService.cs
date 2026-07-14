using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.Common.Security;

public class JwtTokenService(IOptions<JwtOptions> opts) : IJwtTokenService
{
    private readonly JwtOptions _opts = opts.Value;

    private SigningCredentials Credentials() =>
        new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opts.Key)), SecurityAlgorithms.HmacSha256);

    public IssuedToken CreateAccessToken(Guid userId, string email)
    {
        var expires = DateTime.UtcNow.AddMinutes(_opts.AccessTokenMinutes);
        var jwt = new JwtSecurityToken(
            issuer: _opts.Issuer,
            audience: _opts.Audience,
            claims: new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, "User"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            },
            expires: expires,
            signingCredentials: Credentials());
        return new IssuedToken(new JwtSecurityTokenHandler().WriteToken(jwt), expires);
    }

    public (string RawToken, string TokenHash, DateTime ExpiresAt) CreateRefreshToken()
    {
        Span<byte> bytes = stackalloc byte[64];
        RandomNumberGenerator.Fill(bytes);
        var raw = Convert.ToBase64String(bytes);
        return (raw, HashRefreshToken(raw), DateTime.UtcNow.AddDays(_opts.RefreshTokenDays));
    }

    public string HashRefreshToken(string rawToken)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawToken));
        return Convert.ToHexString(bytes);
    }

    public IssuedToken CreateResetToken(string email)
    {
        var expires = DateTime.UtcNow.AddMinutes(_opts.ResetTokenMinutes);
        var jwt = new JwtSecurityToken(
            issuer: _opts.Issuer,
            audience: _opts.Audience,
            claims: new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim("purpose", "reset"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            },
            expires: expires,
            signingCredentials: Credentials());
        return new IssuedToken(new JwtSecurityTokenHandler().WriteToken(jwt), expires);
    }

    public string? ValidateResetToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        try
        {
            var principal = handler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _opts.Issuer,
                ValidAudience = _opts.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opts.Key)),
                ClockSkew = TimeSpan.Zero
            }, out _);

            if (principal.FindFirst("purpose")?.Value != "reset") return null;
            return principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        }
        catch
        {
            return null;
        }
    }
}

using System.Security.Cryptography;
using System.Text;

namespace AuthService.Common.Security;

public static class OtpHasher
{
    public static string Hash(string code)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(code));
        return Convert.ToHexString(bytes);
    }

    public static string GenerateSixDigitCode()
    {
        var value = RandomNumberGenerator.GetInt32(0, 1_000_000);
        return value.ToString("D6");
    }
}

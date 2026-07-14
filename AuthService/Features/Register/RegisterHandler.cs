using AuthService.Common.Api;
using AuthService.Common.Persistence;
using AuthService.Common.Security;
using AuthService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Features.Register;

public class RegisterHandler(IRepository repository, IPasswordHasher hasher)
    : IRequestHandler<RegisterCommand, RegisterResultDto>
{
    public async Task<RegisterResultDto> Handle(RegisterCommand cmd, CancellationToken ct)
    {
        var email = cmd.Email.Trim().ToLowerInvariant();

        var user = new User
        {
            Email            = email,
            PasswordHash     = hasher.Hash(cmd.Password),
            FirstName        = cmd.FirstName.Trim(),
            LastName         = cmd.LastName.Trim(),
            PhoneNumber      = cmd.PhoneNumber.Trim(),
            ProfileCompleted = false,
            IsLockedOut      = false,
            CreatedAt        = DateTime.UtcNow
        };

        repository.Add(user);

        try
        {
            await repository.SaveChangesAsync(ct);
        }
        catch (DbUpdateException)
        {
            throw new AppException(409, ErrorCodes.AUTH_EMAIL_EXISTS, "Email already registered.");
        }

        return new RegisterResultDto(user.Id, RequiresProfileCompletion: true);
    }
}

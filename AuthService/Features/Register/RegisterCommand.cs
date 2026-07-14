using MediatR;

namespace AuthService.Features.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string PhoneNumber) : IRequest<RegisterResultDto>;

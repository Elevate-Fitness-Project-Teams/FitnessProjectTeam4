namespace AuthService.Features.Register;

public record RegisterDto(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string PhoneNumber);

public record RegisterResultDto(Guid UserId, bool RequiresProfileCompletion);

namespace AuthService.Common.Messaging.Events;

public record UserRegisteredEvent(
    Guid UserId,
    string Email,
    string FirstName,
    string LastName,
    string PhoneNumber,
    DateTime OccurredAt);

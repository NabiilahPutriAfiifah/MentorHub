namespace MentorHub.API.DTOs.Auth;

public record AuthResponse(
    string Token,
    Guid UserId,
    Guid RoleId,
    string Username
);
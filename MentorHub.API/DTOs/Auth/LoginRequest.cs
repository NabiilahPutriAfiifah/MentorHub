namespace MentorHub.API.DTOs.Auth;

public record LoginRequest(
    string Username,
    string Password
);
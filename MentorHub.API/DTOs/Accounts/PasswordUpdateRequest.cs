namespace MentorHub.API.DTOs.Accounts;

public record PasswordUpdateRequest(
    string CurrentPassword,
    string NewPassword
);

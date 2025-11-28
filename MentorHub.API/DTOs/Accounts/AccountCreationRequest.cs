namespace MentorHub.API.DTOs.Accounts;

public record AccountCreationRequest
(
    string Username,
    string Password,
    Guid RoleId
);
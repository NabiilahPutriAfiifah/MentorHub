namespace MentorHub.API.DTOs.Accounts;

public record exAccountResponse(
    Guid Id,
    string Username,
    Guid RoleId,

    string FirstName,
    string LastName,
    string Email,
    string Bio,
    string Experience,
    string Position,
    Guid MentorId
);

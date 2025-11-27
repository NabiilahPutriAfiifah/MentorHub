namespace MentorHub.API.DTOs.Accounts;

public record AccountRequest(
    string Username,
    string Password,

    string FirstName,
    string LastName,
    string Email,
    string Bio,
    string Experience,
    string Position
);

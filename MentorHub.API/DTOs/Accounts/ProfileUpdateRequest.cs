namespace MentorHub.API.DTOs.Accounts;

public record ProfileUpdateRequest(
    string FirstName,
    string LastName,
    string Email,
    string Bio,
    string Experience,
    string Position
);

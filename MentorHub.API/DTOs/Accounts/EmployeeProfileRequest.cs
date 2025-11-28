namespace MentorHub.API.DTOs.Accounts;

public record EmployeeProfileRequest(
    string FirstName,
    string LastName,
    string Email,
    string Bio,
    string Experience,
    string Position,
    Guid? MentorId
);
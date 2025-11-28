using MentorHub.API.DTOs.Accounts;
using MentorHub.API.Models;

namespace MentorHub.API.Services.Interfaces;

public interface IAccountService
{
    Task CreateEmployeeAsync(AccountCreationRequest accountRequest, EmployeeProfileRequest profileRequest, CancellationToken cancellationToken);
    Task DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken);
}

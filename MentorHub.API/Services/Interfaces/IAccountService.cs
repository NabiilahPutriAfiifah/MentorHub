using MentorHub.API.DTOs.Accounts;
using MentorHub.API.Models;

namespace MentorHub.API.Services.Interfaces;

public interface IAccountService
{
    Task CreateEmployeeAsync(AccountRequest request, CancellationToken cancellationToken);
    Task UpdateEmployeeAsync(Guid id, AccountRequest request, CancellationToken cancellationToken);
    Task DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<AccountResponse>> GetAllEmployeesAsync(CancellationToken cancellationToken);
    Task<AccountResponse?> GetEmployeeByIdAsync(Guid id, CancellationToken cancellationToken);
}

using MentorHub.API.DTOs.Accounts;

namespace MentorHub.API.Services.Interfaces;

public interface IEmployeeService
{
    Task UpdateProfileAsync(Guid userId, ProfileUpdateRequest request, CancellationToken cancellationToken);
    Task UpdatePasswordAsync(Guid userId, PasswordUpdateRequest request, CancellationToken cancellationToken);
    Task<AccountResponse?> GetProfileByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<AccountResponse>> GetAllProfilesAsync(CancellationToken cancellationToken);
}

using MentorHub.API.DTOs.Roles;

namespace MentorHub.API.Services.Interfaces;

public interface IRoleService
{
    Task CreateRoleAsync(RoleRequest request, CancellationToken cancellationToken);
    Task UpdateRoleAsync(Guid id, RoleRequest request, CancellationToken cancellationToken);
    Task DeleteRoleAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<RoleResponse>> GetAllRolesAsync(CancellationToken cancellationToken);
    Task<RoleResponse?> GetRoleByIdAsync(Guid id, CancellationToken cancellationToken);
}

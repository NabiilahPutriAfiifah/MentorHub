using MentorHub.API.DTOs.Roles;
using MentorHub.API.Models;
using MentorHub.API.Repositories;
using MentorHub.API.Repositories.Interfaces;
using MentorHub.API.Services.Interfaces;

namespace MentorHub.API.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    {
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateRoleAsync(RoleRequest request, CancellationToken cancellationToken)
    {
        var role = new Roles
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
        };

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _roleRepository.CreateAsync(role, cancellationToken);
        }, cancellationToken);
    }

    public async Task DeleteRoleAsync(Guid id, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(id, cancellationToken);
        if (role is null)
        {
            throw new Exception("Role Id not found");
        }

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _roleRepository.DeleteAsync(role);
        }, cancellationToken);
    }

    public async Task<IEnumerable<RoleResponse>> GetAllRolesAsync(CancellationToken cancellationToken)
    {
        var getAllRoles = await _roleRepository.GetAllAsync(cancellationToken);
        if (!getAllRoles.Any())
        {
            throw new Exception("No Roles Found");
        }

        var roleMap = getAllRoles.Select(role => new RoleResponse
        (
            role.Id,
            role.Name
        ));

        return roleMap;
    }

    public async Task<RoleResponse?> GetRoleByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var getRole = await _roleRepository.GetByIdAsync(id, cancellationToken);
        if (getRole is null)
        {
            throw new Exception("Role Id not found");
        }

        var roleMap = new RoleResponse
        (
            getRole.Id,
            getRole.Name
        );

        return roleMap;
    }

    public async Task UpdateRoleAsync(Guid id, RoleRequest request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(id, cancellationToken);

        if (role is null)
        {
            throw new Exception("Role Id not found");
        }

        role.Name = request.Name;

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _roleRepository.UpdateAsync(role);
        }, cancellationToken);
    }

}
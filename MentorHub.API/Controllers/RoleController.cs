using MentorHub.API.DTOs.Roles;
using MentorHub.API.Services.Interfaces;
using MentorHub.API.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace MentorHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        var roles = await _roleService.GetAllRolesAsync(cancellationToken);
        if (!roles.Any())
        {
            return NotFound("No Roles Found"); 
        }
        return Ok(new ApiResponse<IEnumerable<RoleResponse>>(roles));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleById(Guid id, CancellationToken cancellationToken)
    {
        var role = await _roleService.GetRoleByIdAsync(id, cancellationToken);
        if (role is null)
        {
            return NotFound("Role Id not found");
        }
        return Ok(new ApiResponse<RoleResponse?>(role));
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] RoleRequest request, CancellationToken cancellationToken)
    {
        await _roleService.CreateRoleAsync(request, cancellationToken);
        return Ok(new ApiResponse<string>("Role created successfully"));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRole(Guid id, [FromBody] RoleRequest request, CancellationToken cancellationToken)
    {
        await _roleService.UpdateRoleAsync(id, request, cancellationToken);
        return Ok(new ApiResponse<string>("Role updated successfully"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(Guid id, CancellationToken cancellationToken)
    {
        await _roleService.DeleteRoleAsync(id, cancellationToken);
        return Ok(new ApiResponse<string>("Role deleted successfully"));
    }
    
}

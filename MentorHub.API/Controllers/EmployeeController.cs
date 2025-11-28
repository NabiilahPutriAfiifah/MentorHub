using System.Security.Claims;
using MentorHub.API.DTOs.Accounts;
using MentorHub.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MentorHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IEmployeeService _employeeService;

    // Konstruktor (Dependency Injection)
    public EmployeeController(IAccountService accountService, IEmployeeService employeeService)
    {
        _accountService = accountService;
        _employeeService = employeeService;
    }

    private Guid GetUserIdFromToken()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null || !Guid.TryParse(userIdClaim, out Guid userId))
        {
            throw new UnauthorizedAccessException("Invalid user ID in token.");
        }
        return userId;
    }

    // --- 1. ADMIN: CREATE (Membuat Akun/Employee Baru) ---
    // Endpoint ini menerima satu DTO gabungan untuk memudahkan Admin
    public record CombinedCreationRequest(
        AccountCreationRequest AccountData,
        EmployeeProfileRequest ProfileData
    );

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateEmployee([FromBody] CombinedCreationRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // Memanggil AccountService untuk mengkoordinasikan pembuatan ID bersama
            await _accountService.CreateEmployeeAsync(request.AccountData, request.ProfileData, cancellationToken);
            return StatusCode(201); // Created
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Creation failed: " + ex.Message });
        }
    }
    
    // --- 2. ADMIN: DELETE (Menghapus Akun/Employee) ---
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteEmployee(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _accountService.DeleteEmployeeAsync(id, cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    // --- 3. ADMIN: READ ALL (Mendapatkan Semua Profil) ---
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAllEmployees(CancellationToken cancellationToken)
    {
        // Asumsi IEmployeeService.GetAllProfilesAsync sudah ada
        var profiles = await _employeeService.GetAllProfilesAsync(cancellationToken);
        return Ok(profiles);
    }
    
    // --- 4. ADMIN: READ BY ID (Mendapatkan Profil Admin/User Lain) ---
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<AccountResponse>> GetEmployeeById(Guid id, CancellationToken cancellationToken)
    {
        var profile = await _employeeService.GetProfileByIdAsync(id, cancellationToken);
        if (profile is null)
        {
            return NotFound();
        }
        return Ok(profile);
    }

    // --- 5. SELF-SERVICE: READ MY PROFILE (Endpoint /me) ---
    [HttpGet("me")]
    public async Task<ActionResult<AccountResponse>> GetMyProfile(CancellationToken cancellationToken)
    {
        var userId = GetUserIdFromToken();
        var profile = await _employeeService.GetProfileByIdAsync(userId, cancellationToken);
        
        if (profile is null)
        {
            return NotFound(new { message = "User profile not found." });
        }
        return Ok(profile);
    }

    // --- 6. SELF-SERVICE: UPDATE PROFILE (/me/profile) ---
    [HttpPut("me/profile")]
    public async Task<IActionResult> UpdateMyProfile([FromBody] ProfileUpdateRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserIdFromToken();
        await _employeeService.UpdateProfileAsync(userId, request, cancellationToken);
        return NoContent();
    }

    // --- 7. SELF-SERVICE: UPDATE PASSWORD (/me/password) ---
    [HttpPut("me/password")]
    public async Task<IActionResult> UpdateMyPassword([FromBody] PasswordUpdateRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserIdFromToken();
        await _employeeService.UpdatePasswordAsync(userId, request, cancellationToken);
        return NoContent();
    }
}

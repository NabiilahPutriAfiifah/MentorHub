using MentorHub.API.DTOs.Auth;

namespace MentorHub.API.Services.Interfaces;

public interface IAuthenticationService
{
    Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
}

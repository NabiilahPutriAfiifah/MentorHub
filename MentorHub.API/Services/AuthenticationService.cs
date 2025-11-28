using MentorHub.API.DTOs.Auth;
using MentorHub.API.Repositories.Interfaces;
using MentorHub.API.Services.Interfaces;

namespace MentorHub.API.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAccountRepository _accountRepository;
    public AuthenticationService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByUsernameAsync(request.Username, cancellationToken);
        if (account is null)
        {
            throw new Exception("Invalid credentials.");
        }

        var token = "DUMMY_JWT_TOKEN"; 

        return new AuthResponse(
            Token: token,
            UserId: account.Id,
            RoleId: account.RoleId,
            Username: account.Username
        );
    }

}

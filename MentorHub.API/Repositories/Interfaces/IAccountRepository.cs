using MentorHub.API.Models;

namespace MentorHub.API.Repositories.Interfaces;

public interface IAccountRepository : IRepository<Accounts>
{
    Task<Accounts?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
}

using MentorHub.API.Data;
using MentorHub.API.Models;
using MentorHub.API.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using MentorHub.API.Repositories.Interfaces;


namespace MentorHub.API.Repositories.Interfaces;

public class AccountRepository : Repository<Accounts>, IAccountRepository
{
    public AccountRepository(MentorHubDbContext context): base(context)
    {
    }
}

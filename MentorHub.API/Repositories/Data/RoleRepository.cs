using MentorHub.API.Data;
using MentorHub.API.Models;
using MentorHub.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorHub.API.Repositories.Data;

public class RoleRepository : Repository<Roles>, IRoleRepository
{
    public RoleRepository(MentorHubDbContext context) : base(context)
    {
    }
}

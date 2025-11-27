using MentorHub.API.Data;
using MentorHub.API.Models;
using MentorHub.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorHub.API.Repositories.Data;

public class SkillRepository : Repository<Skills>, ISkillRepository
{
    public SkillRepository(MentorHubDbContext context) : base(context)
    {
    }
}

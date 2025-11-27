using MentorHub.API.Data;
using MentorHub.API.Models;
using MentorHub.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorHub.API.Repositories.Data;

public class MentorSkillRepository : Repository<MentorSkills>, IMentorSkillRepository
{
    public MentorSkillRepository(MentorHubDbContext context) : base(context)
    {
    }
}

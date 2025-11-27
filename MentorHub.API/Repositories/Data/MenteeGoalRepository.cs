using MentorHub.API.Data;
using MentorHub.API.Models;
using MentorHub.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorHub.API.Repositories.Data;

public class MenteeGoalRepository : Repository<MenteeGoals>, IMenteeGoalRepository
{
    public MenteeGoalRepository(MentorHubDbContext context) : base(context)
    {
    }
}

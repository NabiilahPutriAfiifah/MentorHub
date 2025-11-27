using MentorHub.API.Data;
using MentorHub.API.Models;
using MentorHub.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorHub.API.Repositories.Data;

public class LearningGoalsRepository : Repository<LearningGoals>, ILearningGoalRepository
{
    public LearningGoalsRepository(MentorHubDbContext context) : base(context)
    {
    }
}

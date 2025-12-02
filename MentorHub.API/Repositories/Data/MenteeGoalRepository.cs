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

    public async Task<IEnumerable<MenteeGoals>> GetByMenteeIdAsync(Guid menteeId, CancellationToken cancellationToken)
    {
        return await _context.MenteeGoals
                             .Where(mg => mg.MenteeId == menteeId)
                             .ToListAsync(cancellationToken);
    }

    public async Task<MenteeGoals?> GetByMenteeIdAndLearningIdAsync(Guid menteeId, Guid learningId, CancellationToken cancellationToken)
    {
        return await _context.MenteeGoals
                            .FirstOrDefaultAsync(mg => 
                                mg.MenteeId == menteeId && mg.LearningId == learningId, 
                                cancellationToken
                            );
    }
}

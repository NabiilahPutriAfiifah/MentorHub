using MentorHub.API.Models;

namespace MentorHub.API.Repositories.Interfaces;

public interface IMenteeGoalRepository : IRepository<MenteeGoals>
{
    Task<IEnumerable<MenteeGoals>> GetByMenteeIdAsync(Guid menteeId, CancellationToken cancellationToken);
    Task<MenteeGoals?> GetByMenteeIdAndLearningIdAsync(Guid menteeId, Guid learningId, CancellationToken cancellationToken);
}

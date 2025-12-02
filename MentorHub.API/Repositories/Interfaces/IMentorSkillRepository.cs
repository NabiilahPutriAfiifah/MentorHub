using MentorHub.API.Models;

namespace MentorHub.API.Repositories.Interfaces;

public interface IMentorSkillRepository : IRepository<MentorSkills>
{
    Task<IEnumerable<MentorSkills>> GetSkillsByAccountIdAsync(Guid accountId, CancellationToken cancellationToken);
    Task<MentorSkills?> GetExistingSkillAsync(Guid accountId, Guid skillId, CancellationToken cancellationToken);
}

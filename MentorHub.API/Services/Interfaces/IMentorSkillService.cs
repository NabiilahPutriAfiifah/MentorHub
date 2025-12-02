using MentorHub.API.DTOs.MentorSkill;

namespace MentorHub.API.Services.Interfaces;

public interface IMentorSkillService
{
    Task AddSkillToMentorAsync(Guid accountId, MentorSkillRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<MentorSkillResponse>> GetMentorSkillsAsync(Guid accountId, CancellationToken cancellationToken);
    Task UpdateSkillLevelAsync(Guid accountId, Guid skillId, int newLevel, CancellationToken cancellationToken);
    Task DeleteSkillFromMentorAsync(Guid accountId, Guid skillId, CancellationToken cancellationToken);
}

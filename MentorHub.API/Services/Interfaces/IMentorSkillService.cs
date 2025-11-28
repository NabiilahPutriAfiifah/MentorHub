using MentorHub.API.DTOs.MentorSkill;

namespace MentorHub.API.Services.Interfaces;

public interface IMentorSkillService
{
    Task AddMentorSkillAsync(Guid mentorId, MentorSkillRequest request, CancellationToken cancellationToken);
    Task UpdateSkillLevelAsync(Guid mentorId, Guid skillId, int newLevel, CancellationToken cancellationToken);    Task DeleteSkillFromMentorAsync(Guid mentorId, Guid skillId, CancellationToken cancellationToken);
    Task<IEnumerable<MentorSkillResponse>> GetMentorSkillsAsync(Guid mentorId, CancellationToken cancellationToken);
    Task<MentorSkillResponse?> GetMentorSkillAsync(Guid mentorId,  Guid skillId, CancellationToken cancellationToken);
}

using MentorHub.API.DTOs.LearningGoals;

namespace MentorHub.API.Services.Interfaces;

public interface ISkillService
{
    Task CreeteSkillAsync(SkillRequest request, CancellationToken cancellationToken);
    Task UpdateSkillAsync(Guid id, SkillRequest request, CancellationToken cancellationToken);
    Task DeleteSkillAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<SkillResponse>> GetAllSkillsAsync(CancellationToken cancellationToken);
    Task<SkillResponse?> GetSkillByIdAsync(Guid id, CancellationToken cancellationToken);
}

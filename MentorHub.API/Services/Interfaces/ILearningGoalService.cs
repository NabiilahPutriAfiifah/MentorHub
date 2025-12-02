using MentorHub.API.DTOs.LearningGoals;

namespace MentorHub.API.Services.Interfaces;

public interface ILearningGoalService
{
    Task CreateLearningGoalAsync(LearningGoalsRequest request, CancellationToken cancellationToken);
    Task<IEnumerable<LearningGoalsResponse>> GetAllLearningGoalsAsync(CancellationToken cancellationToken);
    Task<LearningGoalsResponse?> GetLearningGoalByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateLearningGoalAsync(Guid id, LearningGoalsRequest request, CancellationToken cancellationToken);
    Task DeleteLearningGoalAsync(Guid id, CancellationToken cancellationToken);
}

using MentorHub.API.DTOs.MenteeGoals;

namespace MentorHub.API.Services.Interfaces;

public interface IMenteeGoalService
{
    Task SubmitGoalRequestAsync(Guid menteeId, MenteeGoalsRequest request, CancellationToken cancellationToken);
    Task UpdateGoalRequestStatusAsync(Guid menteeId, Guid goalId, MenteeGoalStatusUpdate statusUpdate, CancellationToken cancellationToken);
    Task<IEnumerable<MenteeGoalsResponse>> GetMenteeGoalsAsync(Guid menteeId, CancellationToken cancellationToken);
    Task DeleteGoalFromMenteeAsync(Guid menteeId, Guid learningId, CancellationToken cancellationToken);
}

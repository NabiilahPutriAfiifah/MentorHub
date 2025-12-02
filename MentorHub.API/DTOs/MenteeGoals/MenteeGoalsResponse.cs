namespace MentorHub.API.DTOs.MenteeGoals;

public record MenteeGoalsResponse
(
    Guid Id,
    Guid MenteeId,
    Guid LearningGoalId,
    string RequestStatus
    // string Title,
    // string Description,
    // string Status,
    // DateTime TargetDate
);

namespace MentorHub.API.DTOs.MenteeGoals;

public record MenteeGoalsResponse
(
    Guid Id,
    Guid MenteeId,
    Guid LearningId,
    string Status
);

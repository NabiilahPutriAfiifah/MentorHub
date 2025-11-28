namespace MentorHub.API.DTOs.LearningGoals;

public record LearningGoalsResponse
(
    Guid Id,
    string Title,
    string Description,
    string Status,
    DateTime TargetDate
);

namespace MentorHub.API.DTOs.LearningGoals;

public record LearningGoalsRequest
(
    string Title,
    string Description,
    int Status,
    DateTime TargetDate
);

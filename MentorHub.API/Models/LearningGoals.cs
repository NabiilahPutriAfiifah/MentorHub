namespace MentorHub.API.Models;

public class LearningGoals
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Status Status { get; set; }
    public DateTime TargetDate { get; set; }

    // Cardinality
    // public MenteeGoals? MenteeGoals { get; set; }
    public ICollection<MenteeGoals> MenteeGoals { get; set; }
    // public ICollection<MenteeGoals> MenteeGoals { get; set; } = new List<MenteeGoals>();
}

public enum Status {
    NotStarted,
    Completed,
    InProgress
}

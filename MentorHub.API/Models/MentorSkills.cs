namespace MentorHub.API.Models;

public class MentorSkills
{
    public Guid Id { get; set; }
    public Guid MentorId { get; set; }
    public Guid SkillId { get; set; }
    public Level Level { get; set; }



    // Cardinality
    // public ICollection<Accounts>? Accounts { get; set; }
    public Accounts? Accounts { get; set; }
    public Skills? Skills { get; set; }
    // public ICollection<Skills>? Skills { get; set; }
}

public enum Level {
    Beginner,
    Intermediate,
    Expert
}



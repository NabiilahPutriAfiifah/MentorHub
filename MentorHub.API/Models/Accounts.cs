namespace MentorHub.API.Models;

public class Accounts
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public Guid RoleId { get; set; }

    public uint? Otp { get; set; }
    public DateTime? Expired { get; set; }
    public bool IsUsed { get; set; }


    // Cardinality
    public Employees? Employee { get; set; }
    public Roles? Role { get; set; }
    // public MentorSkills? MentorSkills { get; set; }
    public ICollection<MentorSkills> MentorSkills { get; set; }
    public ICollection<MenteeGoals> MenteeGoals { get; set; }

    // public MenteeGoals? MenteeGoals { get; set; }
}
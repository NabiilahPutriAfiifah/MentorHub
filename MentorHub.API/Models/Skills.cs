namespace MentorHub.API.Models;

public class Skills
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }


    // public MentorSkills? MentorSkills { get; set; }
    public ICollection<MentorSkills> MentorSkills { get; set; }

}

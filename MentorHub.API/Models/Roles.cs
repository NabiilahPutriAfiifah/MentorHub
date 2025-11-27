namespace MentorHub.API.Models;

public class Roles
{
    public Guid Id { get; set; }
    public string Name { get; set; }


    public ICollection<Accounts> Accounts { get; set; }
}

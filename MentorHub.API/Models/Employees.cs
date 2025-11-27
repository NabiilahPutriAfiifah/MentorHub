namespace MentorHub.API.Models;

public class Employees
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Bio { get; set; }
    public string Experience { get; set; }
    public string Position { get; set; }
    public Guid MentorId { get; set; }

    // Cardinality
    public Accounts? Account { get; set; }
    public Employees? Mentor { get; set;}
    public ICollection<Employees>? Employee { get; set;} 
}

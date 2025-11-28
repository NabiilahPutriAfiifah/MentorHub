using MentorHub.API.Models;

namespace MentorHub.API.Data;

public class MentorHubDataSeeder
{
    private static readonly Guid _adminId = new Guid("10000000-0000-0000-0000-000000000001");
    private static readonly Guid _mentorId = new Guid("10000000-0000-0000-0000-000000000002");
    private static readonly Guid _menteeId = new Guid("10000000-0000-0000-0000-000000000003");


    public static List<Roles> GetDefaultRoles()
    {
        return new List<Roles>
        {
            new Roles { Id = new Guid("10000000-0000-0000-0000-000000000001"), Name = "Admin" },
            new Roles { Id = new Guid("10000000-0000-0000-0000-000000000002"), Name = "Mentor" },
            new Roles { Id = new Guid("10000000-0000-0000-0000-000000000003"), Name = "Mentee" }
        };
    }

    // public static List<Accounts> GetDefaultAccounts()
    // {
    //     return new List<Accounts>
    //     {
    //         new Accounts{ Id = Guid.NewGuid(), Username = "admin", Password = "123456", RoleId = new Guid("10000000-0000-0000-0000-000000000001"), IsUsed = false, Expired = null, Otp = null },
    //         new Accounts{ Id = Guid.NewGuid(), Username = "mentor1", Password = "123456", RoleId = new Guid("10000000-0000-0000-0000-000000000002"), IsUsed = false, Expired = null, Otp = null },
    //         new Accounts{ Id = Guid.NewGuid(), Username = "mentee1", Password = "123456", RoleId = new Guid("10000000-0000-0000-0000-000000000003"), IsUsed = false, Expired = null, Otp = null }
    //     };
    // }


    // public static List<Employees> GetDefaultEmployees()
    // {
    //    return new List<Employees>
    //     {
    //         new Employees { Id = Guid.NewGuid(), FirstName = "Alice", LastName = "Wonderland", Email = "alice@gmail.com", Bio = "Senior Developer", Experience = "5 years in software development", Position = "Admin", MentorId = Guid.Empty },
    //         new Employees { Id = Guid.NewGuid(), FirstName = "Bob", LastName = "Builder", Email = "bob.builder@gmail.com", Bio = "Junior Developer", Experience = "1 year in software development", Position = "Mentor", MentorId = Guid.Empty },
    //         new Employees { Id = Guid.NewGuid(), FirstName = "Charlie", LastName = "Chocolate", Email = "charlie02@gmail.com", Bio = "Intern Developer", Experience = "6 months in software development", Position = "Mentee", MentorId = Guid.Empty }
    //     };       
    // }

    public static List<Skills> GetDefaultSkills()
    {
        return new List<Skills>
        {
            new Skills { Id = new Guid("10000000-0000-0000-0000-000000000001"), Name = "C#", Description = "Programming language" },
            new Skills { Id = new Guid("10000000-0000-0000-0000-000000000002"), Name = "JavaScript", Description = "Frontend language" },
            new Skills { Id = new Guid("10000000-0000-0000-0000-000000000003"), Name = "SQL", Description = "Database skill" }
        };
    }
}

using MentorHub.API.Models;

namespace MentorHub.API.Data;

public class MentorHubDataSeeder
{
    // Primary Keys untuk Accounts dan Employees
    private static readonly Guid _adminAccount = new Guid("10000000-0000-0000-0000-000000000001");
    private static readonly Guid _mentorAccount = new Guid("10000000-0000-0000-0000-000000000002");
    private static readonly Guid _menteeAccount = new Guid("10000000-0000-0000-0000-000000000003");

    // ID for Roles
    private static readonly Guid _adminRole = new Guid("10000000-0000-0000-0000-000000000001");
    private static readonly Guid _mentorRole = new Guid("10000000-0000-0000-0000-000000000002");
    private static readonly Guid _menteeRole = new Guid("10000000-0000-0000-0000-000000000003");

    // ID for Skills
    private static readonly Guid _skillIdCs = new Guid("10000000-0000-0000-0000-000000000001");
    private static readonly Guid _skillIdJs = new Guid("10000000-0000-0000-0000-000000000002");
    private static readonly Guid _skillIdSql = new Guid("10000000-0000-0000-0000-000000000003");
    private static readonly Guid _skillIdUx = new Guid("10000000-0000-0000-0000-000000000004");
    private static readonly Guid _skillIdPy = new Guid("10000000-0000-0000-0000-000000000005");

    // ID for Learning Goals
    private static readonly Guid _goalId1 = new Guid("10000000-0000-0000-0000-000000000010");


    public static List<Roles> GetDefaultRoles()
    {
        return new List<Roles>
        {
            new Roles { Id = _adminRole, Name = "Admin" },
            new Roles { Id = _mentorRole, Name = "Mentor" },
            new Roles { Id = _menteeRole, Name = "Mentee" }
        };
    }

    public static List<Skills> GetDefaultSkills()
    {
        return new List<Skills>
        {
            new Skills { Id = _skillIdCs, Name = "C#", Description = "Programming language for backend development." },
            new Skills { Id = _skillIdJs, Name = "JavaScript", Description = "Frontend and full-stack language." },
            new Skills { Id = _skillIdSql, Name = "SQL", Description = "Database query and management skill." },
            new Skills { Id = _skillIdUx, Name = "UX/UI Design", Description = "User experience and interface design." },
            new Skills { Id = _skillIdPy, Name = "Python", Description = "Scripting and data analysis language." }
        };
    }

    public static List<Accounts> GetDefaultAccounts()
    {
        const string defaultPassword = "Pa$$word123";

        return new List<Accounts>
        {
            new Accounts {
                Id = _adminAccount,
                Username = "admin",
                Password = defaultPassword,
                RoleId = _adminRole
            },
            new Accounts {
                Id = _mentorAccount,
                Username = "mentor",
                Password = defaultPassword,
                RoleId = _mentorRole
            },
            new Accounts {
                Id = _menteeAccount,
                Username = "mentee",
                Password = defaultPassword,
                RoleId = _menteeRole
            }
        };
    }

    public static List<Employees> GetDefaultEmployees()
    {
        return new List<Employees>
        {
            new Employees {
                Id = _adminAccount,
                FirstName = "Super",
                LastName = "Admin",
                Email = "admin@hub.com",
                Bio = "System Administrator.",
                Experience = "Senior",
                Position = "System Analyst",
                MentorId = null
            },
            new Employees {
                Id = _mentorAccount,
                FirstName = "Budi",
                LastName = "Prasetyo",
                Email = "budi@hub.com",
                Bio = "Full-stack developer with 5 years experience.",
                Experience = "Expert",
                Position = "Tech Lead",
                MentorId = null
            },
            new Employees {
                Id = _menteeAccount,
                FirstName = "Siti",
                LastName = "Aisyah",
                Email = "siti@hub.com",
                Bio = "Junior developer learning C#.",
                Experience = "Junior",
                Position = "Developer",
                MentorId = _mentorAccount
            }
        };
    }

    public static List<MentorSkills> GetDefaultMentorSkills()
    {
        return new List<MentorSkills>
        {
            new MentorSkills {
                Id = Guid.NewGuid(),
                MentorId = _mentorAccount,
                SkillId = _skillIdCs,
                Level = Level.Expert
            },
            new MentorSkills {
                Id = Guid.NewGuid(),
                MentorId = _mentorAccount,
                SkillId = _skillIdJs,
                Level = Level.Intermediate
            }
        };
    }

    public static List<LearningGoals> GetDefaultLearningGoals()
    {
        return new List<LearningGoals>
        {
            new LearningGoals {
                Id = _goalId1,
                Title = "Menguasai EF Core",
                Description = "Mampu mengimplementasikan CRUD dengan Entity Framework Core di ASP.NET.",
                TargetDate = DateTime.Today.AddDays(30),
                Status = Status.InProgress
            }
        };
    }

    public static List<MenteeGoals> GetDefaultMenteeGoals()
    {
        return new List<MenteeGoals>
        {
            new MenteeGoals
            {
                Id = Guid.NewGuid(),
                MenteeId = _menteeAccount,
                LearningId = _goalId1,
                Status = RequestStatus.Approved
            }
        };
    }
}
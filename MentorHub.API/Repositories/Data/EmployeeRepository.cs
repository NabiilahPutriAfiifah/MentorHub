using MentorHub.API.Data;
using MentorHub.API.Models;
using MentorHub.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorHub.API.Repositories.Data;

public class EmployeeRepository : Repository<Employees>, IEmployeeRepository
{
    public EmployeeRepository(MentorHubDbContext context) : base(context)
    {
    }
}

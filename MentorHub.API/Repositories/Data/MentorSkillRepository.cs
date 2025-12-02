using MentorHub.API.Data;
using MentorHub.API.Models;
using MentorHub.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorHub.API.Repositories.Data;

public class MentorSkillRepository : Repository<MentorSkills>, IMentorSkillRepository
{
    public MentorSkillRepository(MentorHubDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<MentorSkills>> GetSkillsByAccountIdAsync(Guid accountId, CancellationToken cancellationToken)
    {
        return await _context.Set<MentorSkills>()
                             .Where(ms => ms.MentorId == accountId)
                             .Include(ms => ms.Skills) 
                             .ToListAsync(cancellationToken);
    }

    public async Task<MentorSkills?> GetExistingSkillAsync(Guid accountId, Guid skillId, CancellationToken cancellationToken)
    {
        return await _context.Set<MentorSkills>()
                             .Where(ms => ms.MentorId == accountId && ms.SkillId == skillId)
                             .Include(ms => ms.Skills) 
                             .FirstOrDefaultAsync(cancellationToken);
    }
}

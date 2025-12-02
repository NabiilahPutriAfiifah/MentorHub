using MentorHub.API.DTOs.MentorSkill;
using MentorHub.API.Models;
using MentorHub.API.Repositories;
using MentorHub.API.Repositories.Interfaces;
using MentorHub.API.Services.Interfaces;

namespace MentorHub.API.Services;

public class MentorSkillService : IMentorSkillService
{
    private readonly IMentorSkillRepository _mentorSkillRepository;
    private readonly ISkillRepository _skillRepository; 
    private readonly IUnitOfWork _unitOfWork;

    public MentorSkillService(IMentorSkillRepository mentorSkillRepository, ISkillRepository skillRepository, IUnitOfWork unitOfWork)
    {
        _mentorSkillRepository = mentorSkillRepository;
        _skillRepository = skillRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddSkillToMentorAsync(Guid accountId, MentorSkillRequest request, CancellationToken cancellationToken)
    {
        var skillMaster = await _skillRepository.GetByIdAsync(request.SkillId, cancellationToken);
        if (skillMaster is null)
        {
            throw new NullReferenceException("Skill ID not found in master list.");
        }

        var existingSkill = await _mentorSkillRepository.GetExistingSkillAsync(accountId, request.SkillId, cancellationToken);
        if (existingSkill is not null)
        {
            throw new ArgumentException("Mentor already has this skill.");
        }
        
        if (!Enum.IsDefined(typeof(Level), request.Level))
        {
            throw new ArgumentException("Invalid level value.");
        }
        var skillLevel = (Level)request.Level;

        var mentorSkill = new MentorSkills
        {
            MentorId = accountId, 
            SkillId = request.SkillId,
            Level = skillLevel 
        };

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _mentorSkillRepository.CreateAsync(mentorSkill, cancellationToken);
        }, cancellationToken);
    }

    public async Task DeleteSkillFromMentorAsync(Guid accountId, Guid skillId, CancellationToken cancellationToken)
    {
        var mentorSkill = await _mentorSkillRepository.GetExistingSkillAsync(accountId, skillId, cancellationToken);
        
        if (mentorSkill is null)
        {
            throw new NullReferenceException("Skill not found for this mentor.");
        }

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _mentorSkillRepository.DeleteAsync(mentorSkill); 
        }, cancellationToken);
    }

    public async Task<IEnumerable<MentorSkillResponse>> GetMentorSkillsAsync(Guid accountId, CancellationToken cancellationToken)
    {
        var mentorSkills = await _mentorSkillRepository.GetSkillsByAccountIdAsync(accountId, cancellationToken);
        
        if (!mentorSkills.Any())
        {
            return Enumerable.Empty<MentorSkillResponse>();
        }

        return mentorSkills.Select(ms => new MentorSkillResponse(
            ms.SkillId,
            ms.Skills.Name, 
            ms.Skills.Description ?? string.Empty, 
            ms.Level.ToString() 
        ));
    }

    public async Task UpdateSkillLevelAsync(Guid accountId, Guid skillId, int newLevel, CancellationToken cancellationToken)
    {
        if (!Enum.IsDefined(typeof(Level), newLevel))
        {
            throw new ArgumentException("Invalid skill level value.");
        }
        var skillLevel = (Level)newLevel;

        var mentorSkill = await _mentorSkillRepository.GetExistingSkillAsync(accountId, skillId, cancellationToken);

        if (mentorSkill is null)
        {
            throw new NullReferenceException("Skill not found for this mentor.");
        }

        mentorSkill.Level = skillLevel;

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _mentorSkillRepository.UpdateAsync(mentorSkill); 
        }, cancellationToken);
    }

}
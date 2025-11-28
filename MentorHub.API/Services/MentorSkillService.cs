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

    public async Task AddMentorSkillAsync(Guid mentorId, MentorSkillRequest request, CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.GetByIdAsync(request.SkillId, cancellationToken);
        if (skill is null)
        {
            throw new Exception("Skill Id not found");
        }

        if (!Enum.TryParse(request.Level, true, out MentorHub.API.Models.Level skillLevel))
        {
            throw new Exception($"Invalid skill level value provided: {request.Level}. Must be one of {string.Join(", ", Enum.GetNames(typeof(MentorHub.API.Models.Level)))}.");
        }
        
        var mentorSkill = new MentorSkills
        {
            Id = Guid.NewGuid(),
            MentorId = mentorId, 
            SkillId = request.SkillId,
            Level = skillLevel 
        };

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _mentorSkillRepository.CreateAsync(mentorSkill, cancellationToken);
        }, cancellationToken);
    }

    public async Task DeleteSkillFromMentorAsync(Guid mentorId, Guid skillId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        // var mentorSkill = await _mentorSkillRepository.GetMentorSkillAsync(mentorId, skillId, cancellationToken);
    
        // if (mentorSkill is null)
        // {
        //     throw new Exception("Mentor Skill not found");
        // }

        // await _unitOfWork.CommitTransactionAsync(async () =>
        // {
        //     await _mentorSkillRepository.DeleteAsync(mentorSkill); 
        // }, cancellationToken);
    }

    public async Task<MentorSkillResponse?> GetMentorSkillAsync(Guid mentorId, Guid skillId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        // var mentorSkill = await _mentorSkillRepository.GetMentorSkillAsync(mentorId, skillId, cancellationToken);

        // if (mentorSkill is null)
        // {
        //     return null; 
        // }

        // return new MentorSkillResponse(
        //     mentorSkill.SkillId,
        //     mentorSkill.Skills.Name,
        //     mentorSkill.Level.ToString()
        // );
    }


    public async Task<IEnumerable<MentorSkillResponse>> GetMentorSkillsAsync(Guid mentorId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        // var mentorSkills = await _mentorSkillRepository.GetMentorSkillsAsync(mentorId, cancellationToken);
        
        // if (!mentorSkills.Any())
        // {
        //     return Enumerable.Empty<MentorSkillResponse>(); 
        // }

        // return mentorSkills.Select(ms => new MentorSkillResponse
        // (
        //     ms.SkillId,
        //     ms.Skills.Name,
        //     ms.Level.ToString() 
        // ));
    }
    
    public async Task UpdateSkillLevelAsync(Guid mentorId, Guid skillId, int newLevel, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        // if (!Enum.IsDefined(typeof(MentorHub.API.Models.Level), newLevel))
        // {
        //     throw new Exception("Invalid skill level value.");
        // }
        // var skillLevel = (MentorHub.API.Models.Level)newLevel;

        // var mentorSkill = await _mentorSkillRepository.GetMentorSkillAsync(mentorId, skillId, cancellationToken);

        // if (mentorSkill is null)
        // {
        //     throw new Exception("Skill not found for this mentor.");
        // }

        // mentorSkill.Level = skillLevel;

        // await _unitOfWork.CommitTransactionAsync(async () =>
        // {
        //     await _mentorSkillRepository.UpdateAsync(mentorSkill);
        // }, cancellationToken);
    }

}

using MentorHub.API.DTOs.LearningGoals;
using MentorHub.API.Models;
using MentorHub.API.DTOs.Roles;
using MentorHub.API.Repositories;
using MentorHub.API.Repositories.Interfaces;
using MentorHub.API.Services.Interfaces;

namespace MentorHub.API.Services;

public class SkillService : ISkillService
{
    private readonly ISkillRepository _skillRepository;
    private readonly IUnitOfWork _unitOfWork;
    public SkillService(ISkillRepository skillRepository, IUnitOfWork unitOfWork)
    {
        _skillRepository = skillRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreeteSkillAsync(SkillRequest request, CancellationToken cancellationToken)
    {
        var skill = new Skills
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description
        };

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _skillRepository.CreateAsync(skill, cancellationToken);
        }, cancellationToken);
    }

    public async Task DeleteSkillAsync(Guid id, CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.GetByIdAsync(id, cancellationToken);
        if (skill is null)
        {
            throw new NullReferenceException("Skill Id not found");
        }

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _skillRepository.DeleteAsync(skill);
        }, cancellationToken);
    }

    public async Task<IEnumerable<SkillResponse>> GetAllSkillsAsync(CancellationToken cancellationToken)
    {
        var getAllSkills = await _skillRepository.GetAllAsync(cancellationToken);
        if (!getAllSkills.Any())
        {
            throw new NullReferenceException("No Skills Found");
        }
        var skillMap = getAllSkills.Select(skill => new SkillResponse
        (
            skill.Id,
            skill.Name,
            skill.Description
        ));
        return skillMap;
    }

    public async Task<SkillResponse?> GetSkillByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.GetByIdAsync(id, cancellationToken);
        if (skill is null)
        {
            throw new NullReferenceException("Skill Id not found");
        }
        var skillResponse = new SkillResponse
        (
            skill.Id,
            skill.Name,
            skill.Description
        );
        return skillResponse;
    }

    public async Task UpdateSkillAsync(Guid id, SkillRequest request, CancellationToken cancellationToken)
    {
        var skill = await _skillRepository.GetByIdAsync(id, cancellationToken);
        if (skill is null)
        {
            throw new NullReferenceException("Skill Id not found");
        }

        skill.Name = request.Name;
        skill.Description = request.Description;

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _skillRepository.UpdateAsync(skill);
        }, cancellationToken);
    }

}

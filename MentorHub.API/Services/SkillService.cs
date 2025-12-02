using MentorHub.API.DTOs.LearningGoals;
using MentorHub.API.Models;
using MentorHub.API.DTOs.Roles;
using MentorHub.API.Repositories;
using MentorHub.API.Repositories.Interfaces;
using MentorHub.API.Services.Interfaces;
using MentorHub.API.Utilities;
using Microsoft.CodeAnalysis;

namespace MentorHub.API.Services;

public class SkillService : ISkillService
{
    private readonly ISkillRepository _skillRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailHandler _emailHandler;

    public SkillService(ISkillRepository skillRepository, IUnitOfWork unitOfWork, IEmailHandler emailHandler)
    {
        _skillRepository = skillRepository;
        _unitOfWork = unitOfWork;
        _emailHandler = emailHandler;
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

        _ = SendProjectCreationEmailAsync(skill);

        // var emailDto = new EmailDto(
        //         To: "admin@skillsync.local",
        //         Subject: "Hallooooooo",
        //         Body: "hallo world"
        //     );

        //     await _emailHandler.EmailAsync(emailDto);

    }


    private async Task SendProjectCreationEmailAsync(Skills project)
    {
        try
        {
            var emailBody =
                $@"
                <h2>New Project Created</h2>
                <p><strong>Project Name:</strong> {project.Name}</p>
                <p><strong>Description:</strong> {project.Description}</p>
                <hr>
                <p>Project ID: {project.Id}</p>
            ";

            var emailDto = new EmailDto(
                To: "admin@skillsync.local",
                Subject: $"New Project Created: {project.Name}",
                Body: emailBody
            );

            await _emailHandler.EmailAsync(emailDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to send project creation email: {ex.Message}");
        }
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

using FluentValidation;
using MentorHub.API.DTOs.LearningGoals;

public class SkillRequestValidator : AbstractValidator<SkillRequest>
{
    public SkillRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Skill name is required.")
            .MinimumLength(2)
            .WithMessage("Skill name must be at least 2 characters long.")
            .MaximumLength(100)
            .WithMessage("Skill name must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Skill description must not exceed 500 characters.");
    }

}
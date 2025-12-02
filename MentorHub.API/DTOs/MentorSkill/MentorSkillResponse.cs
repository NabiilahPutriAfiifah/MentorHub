namespace MentorHub.API.DTOs.MentorSkill;

public record MentorSkillResponse
(
    Guid SkillId,
    string SkillName,
    string SkillDescription,
    string Level
);

using MentorHub.API.DTOs.LearningGoals;
using MentorHub.API.Services.Interfaces;
using MentorHub.API.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace MentorHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SkillController : ControllerBase
{
    private readonly ISkillService _skillService;

    public SkillController(ISkillService skillService)
    {
        _skillService = skillService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSkills(CancellationToken cancellationToken)
    {
        var skills = await _skillService.GetAllSkillsAsync(cancellationToken);
        if (!skills.Any())
        {
            return NotFound("No Skills Found");
        }
        return Ok(new ApiResponse<IEnumerable<SkillResponse>>(skills));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSkillById(Guid id, CancellationToken cancellationToken)
    {
        var skill = await _skillService.GetSkillByIdAsync(id, cancellationToken);
        if (skill is null)
        {
            return NotFound("Skill Id not found"); 
        }
        return Ok(new ApiResponse<SkillResponse?>(skill));
    }

    [HttpPost]
    public async Task<IActionResult> CreateSkill([FromBody] SkillRequest request, CancellationToken cancellationToken)
    {
        await _skillService.CreeteSkillAsync(request, cancellationToken);
        return Ok(new ApiResponse<string>("Skill created successfully"));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSkill(Guid id, [FromBody] SkillRequest request, CancellationToken cancellationToken)
    {
        await _skillService.UpdateSkillAsync(id, request, cancellationToken);
        return Ok(new ApiResponse<string>("Skill updated successfully"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSkill(Guid id, CancellationToken cancellationToken)
    {
        await _skillService.DeleteSkillAsync(id, cancellationToken);
        return Ok(new ApiResponse<string>("Skill deleted successfully"));
    }
}

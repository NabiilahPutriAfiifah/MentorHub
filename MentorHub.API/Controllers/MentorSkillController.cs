using System.Security.Claims;
using MentorHub.API.DTOs.MentorSkill;
using MentorHub.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MentorHub.API.Controllers;

[ApiController]
// [Route("api/[controller]")]
[Route("api/mentors/skills")]
// [Authorize(Roles = "Mentor")]
public class MentorSkillController : ControllerBase
{
    private readonly IMentorSkillService _mentorSkillService;

    public MentorSkillController(IMentorSkillService mentorSkillService)
    {
        _mentorSkillService = mentorSkillService;
    }

    // private Guid GetMentorIdFromToken()
    // {
    //     var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    //     if (userIdClaim == null || !Guid.TryParse(userIdClaim, out Guid mentorId))
    //     {
    //         throw new UnauthorizedAccessException("Invalid mentor ID in token.");
    //     }
    //     return mentorId;
    // }

    private static readonly Guid _dummyMenteeId = new Guid("10000000-0000-0000-0000-000000000003");
    private static readonly Guid _dummyMentorId = new Guid("10000000-0000-0000-0000-000000000002");

    
    // --- C: CREATE
    [HttpPost]
    public async Task<IActionResult> AddSkill([FromBody] MentorSkillRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var mentorId = _dummyMentorId;
            await _mentorSkillService.AddSkillToMentorAsync(mentorId, request, cancellationToken);
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // --- R: READ
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MentorSkillResponse>>> GetSkills(CancellationToken cancellationToken)
    {
        try
        {
            var mentorId = _dummyMentorId;
            var skills = await _mentorSkillService.GetMentorSkillsAsync(mentorId, cancellationToken);
            if (skills is null || !skills.Any())
            {
                return NotFound("No skills found for the mentor.");
            }
            return Ok(skills);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal Server Error: " + ex.Message });
        }
    }
    
    // --- U: UPDATE
    [HttpPut("{skillId}")]
    public async Task<IActionResult> UpdateSkill(Guid skillId, [FromBody] int level, CancellationToken cancellationToken)
    {
        try
        {
            var mentorId = _dummyMentorId;
            await _mentorSkillService.UpdateSkillLevelAsync(mentorId, skillId, level, cancellationToken);
            return NoContent(); 
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    // --- D: DELETE
    [HttpDelete("{skillId}")]
    public async Task<IActionResult> DeleteSkill(Guid skillId, CancellationToken cancellationToken)
    {
        try
        {
            var mentorId = _dummyMentorId;
            await _mentorSkillService.DeleteSkillFromMentorAsync(mentorId, skillId, cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}

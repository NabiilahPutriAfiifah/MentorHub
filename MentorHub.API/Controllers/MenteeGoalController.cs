using System.Security.Claims;
using MentorHub.API.DTOs.MenteeGoals;
using MentorHub.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MentorHub.API.Controllers;

[ApiController]
[Route("api/mentee-goals")] 
[AllowAnonymous]

public class MenteeGoalController : ControllerBase
{
    private readonly IMenteeGoalService _menteeGoalsService;
    
    private static readonly Guid _dummyMenteeId = new Guid("10000000-0000-0000-0000-000000000003");
    private static readonly Guid _dummyMentorId = new Guid("10000000-0000-0000-0000-000000000002");

    public MenteeGoalController(IMenteeGoalService menteeGoalsService)
    {
        _menteeGoalsService = menteeGoalsService;
    }

    [HttpPost]
    // [Authorize(Roles = "Mentee")]
    public async Task<IActionResult> SubmitGoal([FromBody] MenteeGoalsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var menteeId = _dummyMenteeId; 
            await _menteeGoalsService.SubmitGoalRequestAsync(menteeId, request, cancellationToken);
            return StatusCode(201); 
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // 2. REVIEW
    [HttpPut("{goalId}/review")]
    // [Authorize(Roles = "Mentor")] 
    public async Task<IActionResult> ReviewGoal(Guid goalId, [FromBody] MenteeGoalStatusUpdate statusUpdate, CancellationToken cancellationToken)
    {
        try
        {
            var menteeId = _dummyMenteeId; 
            await _menteeGoalsService.UpdateGoalRequestStatusAsync(menteeId, goalId, statusUpdate, cancellationToken); 
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // 3. READ
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MenteeGoalsResponse>>> GetMyGoals(CancellationToken cancellationToken)
    {
        try
        {
            var menteeId = _dummyMenteeId;
            var goals = await _menteeGoalsService.GetMenteeGoalsAsync(menteeId, cancellationToken);
            return Ok(goals);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal Server Error: " + ex.Message });
        }
    }
    
    // 4. DELETE
    [HttpDelete("{goalId}")]
    // [Authorize(Roles = "Mentee")]
    public async Task<IActionResult> DeleteGoal(Guid goalId, CancellationToken cancellationToken)
    {
        try
        {
            var menteeId = _dummyMenteeId;
            await _menteeGoalsService.DeleteGoalFromMenteeAsync(menteeId, goalId, cancellationToken); 
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
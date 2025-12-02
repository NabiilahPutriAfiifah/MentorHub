using MentorHub.API.DTOs.LearningGoals;
using MentorHub.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MentorHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LearningGoalController : ControllerBase
{
    private readonly ILearningGoalService _learningGoalService;

    public LearningGoalController(ILearningGoalService learningGoalService)
    {
        _learningGoalService = learningGoalService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGoal([FromBody] LearningGoalsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _learningGoalService.CreateLearningGoalAsync(request, cancellationToken);
            return StatusCode(201);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    // [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<LearningGoalsResponse>>> GetAllGoals(CancellationToken cancellationToken)
    {
        var goals = await _learningGoalService.GetAllLearningGoalsAsync(cancellationToken);
        return Ok(goals);
    }
    

    [HttpGet("{id}")]
    public async Task<ActionResult<LearningGoalsResponse>> GetGoalById(Guid id, CancellationToken cancellationToken)
    {
        var goal = await _learningGoalService.GetLearningGoalByIdAsync(id, cancellationToken);
        if (goal is null)
        {
            return NotFound();
        }
        return Ok(goal);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGoal(Guid id, [FromBody] LearningGoalsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await _learningGoalService.UpdateLearningGoalAsync(id, request, cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGoal(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _learningGoalService.DeleteLearningGoalAsync(id, cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
using MentorHub.API.DTOs.LearningGoals;
using MentorHub.API.Models;
using MentorHub.API.Repositories;
using MentorHub.API.Repositories.Interfaces;

namespace MentorHub.API.Services.Interfaces;

public class LearningGoalService : ILearningGoalService
{
    private readonly ILearningGoalRepository _learningGoalRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LearningGoalService(ILearningGoalRepository learningGoalRepository, IUnitOfWork unitOfWork)
    {
        _learningGoalRepository = learningGoalRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateLearningGoalAsync(LearningGoalsRequest request, CancellationToken cancellationToken)
    {
        if (!Enum.IsDefined(typeof(Status), request.Status))
        {
            throw new Exception("Invalid status value.");
        }
        var goalStatus = (Status)request.Status;

        var goal = new LearningGoals
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            TargetDate = request.TargetDate, 
            Status = goalStatus
        };

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _learningGoalRepository.CreateAsync(goal, cancellationToken);
        }, cancellationToken);
    }

    public async Task DeleteLearningGoalAsync(Guid id, CancellationToken cancellationToken)
    {
        var goal = await _learningGoalRepository.GetByIdAsync(id, cancellationToken);

        if (goal is null)
        {
            throw new Exception("Learning Goal not found.");
        }

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _learningGoalRepository.DeleteAsync(goal);
        }, cancellationToken);
    }

    public async Task<IEnumerable<LearningGoalsResponse>> GetAllLearningGoalsAsync(CancellationToken cancellationToken)
    {
        var goals = await _learningGoalRepository.GetAllAsync(cancellationToken);
        
        if (!goals.Any())
        {
            return Enumerable.Empty<LearningGoalsResponse>();
        }

        return goals.Select(g => new LearningGoalsResponse(
            g.Id,
            g.Title,
            g.Description,
            g.Status.ToString(), // Konversi Enum ke string
            g.TargetDate
        ));
    }

    public async Task<LearningGoalsResponse?> GetLearningGoalByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var goal = await _learningGoalRepository.GetByIdAsync(id, cancellationToken);
        
        if (goal is null)
        {
            return null;
        }

        return new LearningGoalsResponse(
            goal.Id,
            goal.Title,
            goal.Description,
            goal.Status.ToString(),
            goal.TargetDate
        );
    }

    public async Task UpdateLearningGoalAsync(Guid id, LearningGoalsRequest request, CancellationToken cancellationToken)
    {
        var goal = await _learningGoalRepository.GetByIdAsync(id, cancellationToken);

        if (goal is null)
        {
            throw new Exception("Learning Goal not found.");
        }
        
        if (!Enum.IsDefined(typeof(Status), request.Status))
        {
            throw new Exception("Invalid status value.");
        }
        var goalStatus = (Status)request.Status;

        goal.Title = request.Title;
        goal.Description = request.Description;
        goal.TargetDate = request.TargetDate;
        goal.Status = goalStatus;

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _learningGoalRepository.UpdateAsync(goal);
        }, cancellationToken);
    }

}

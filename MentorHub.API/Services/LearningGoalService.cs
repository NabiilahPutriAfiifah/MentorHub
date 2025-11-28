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
        var learningGoal = new LearningGoals
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
        };

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _learningGoalRepository.CreateAsync(learningGoal, cancellationToken);
        }, cancellationToken);
    }

    public async Task DeleteLearningGoalAsync(Guid id, CancellationToken cancellationToken)
    {
        var learningGoal = await _learningGoalRepository.GetByIdAsync(id, cancellationToken);
        if (learningGoal is null)
        {
            throw new Exception("Learning Goal Id not found");
        }

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _learningGoalRepository.DeleteAsync(learningGoal);
        }, cancellationToken);
    }

    public async Task<IEnumerable<LearningGoalsResponse>> GetAllLearningGoalsAsync(CancellationToken cancellationToken)
    {
        var getAllLearningGoals = await _learningGoalRepository.GetAllAsync(cancellationToken);
        if (!getAllLearningGoals.Any())
        {
            throw new Exception("No Learning Goals Found");
        }
        var learningGoalMap = getAllLearningGoals.Select(learningGoal => new LearningGoalsResponse
        (
            learningGoal.Id,
            learningGoal.Title,
            learningGoal.Description,
            learningGoal.Status.ToString(),
            learningGoal.TargetDate
        ));
        return learningGoalMap;
    }

    public async Task<LearningGoalsResponse?> GetLearningGoalByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var learningGoal = await _learningGoalRepository.GetByIdAsync(id, cancellationToken);
        if (learningGoal is null)
        {
            throw new Exception("Learning Goal Id not found");
        }

        var learningGoalResponse = new LearningGoalsResponse
        (
            learningGoal.Id,
            learningGoal.Title,
            learningGoal.Description,
            learningGoal.Status.ToString(),
            learningGoal.TargetDate
        );

        return learningGoalResponse;
    }

    public async Task UpdateLearningGoalAsync(Guid id, LearningGoalsRequest request, CancellationToken cancellationToken)
    {
        var learningGoal = await _learningGoalRepository.GetByIdAsync(id, cancellationToken);
        if (learningGoal is null)
        {
            throw new Exception("Learning Goal Id not found");
        }

        learningGoal.Title = request.Title;
        learningGoal.Description = request.Description;

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _learningGoalRepository.UpdateAsync(learningGoal);
        }, cancellationToken);
    }

}

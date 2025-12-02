using MentorHub.API.DTOs.MenteeGoals;
using MentorHub.API.Models;
using MentorHub.API.Repositories;
using MentorHub.API.Repositories.Interfaces;
using MentorHub.API.Services.Interfaces;
using MentorHub.API.Utilities;

namespace MentorHub.API.Services;

public class MenteeGoalsService : IMenteeGoalService
{
    private readonly IMenteeGoalRepository _menteeGoalsRepository;
    private readonly ILearningGoalRepository _learningGoalsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailHandler _emailHandler;


    public MenteeGoalsService(IMenteeGoalRepository menteeGoalsRepository, ILearningGoalRepository learningGoalsRepository, IUnitOfWork unitOfWork)
    {
        _menteeGoalsRepository = menteeGoalsRepository;
        _learningGoalsRepository = learningGoalsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task DeleteGoalFromMenteeAsync(Guid menteeId, Guid learningId, CancellationToken cancellationToken)
    {
        var goalToDelete = await _menteeGoalsRepository.GetByMenteeIdAndLearningIdAsync(
            menteeId, 
            learningId, 
            cancellationToken
        );

        if (goalToDelete is null)
        {
            throw new Exception("Mentee Goal not found or already deleted.");
        }

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _menteeGoalsRepository.DeleteAsync(goalToDelete);
        }, cancellationToken);
    }


    public async Task<IEnumerable<MenteeGoalsResponse>> GetMenteeGoalsAsync(Guid menteeId, CancellationToken cancellationToken)
    {
        var menteeGoals = await _menteeGoalsRepository.GetByMenteeIdAsync(menteeId, cancellationToken);

        if (!menteeGoals.Any())
        {
            return Enumerable.Empty<MenteeGoalsResponse>();
        }

        return menteeGoals.Select(mg => new MenteeGoalsResponse(
            mg.Id,
            mg.MenteeId,
            mg.LearningId ?? Guid.Empty,
            mg.Status.ToString()
            
            // mg.LearningGoals.Title,       
            // mg.LearningGoals.Description, 
            // mg.LearningGoals.Status.ToString(), 
            // mg.LearningGoals.TargetDate
        ));
    }

    public async Task SubmitGoalRequestAsync(Guid menteeId, MenteeGoalsRequest request, CancellationToken cancellationToken)
    {
        var menteeGoal = new MenteeGoals
        {
            Id = Guid.NewGuid(),
            MenteeId = menteeId,
            LearningId = request.LearningGoalId,
            Status = RequestStatus.Pending 
        };

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _menteeGoalsRepository.CreateAsync(menteeGoal, cancellationToken);
        }, cancellationToken);
    }

    public async Task UpdateGoalRequestStatusAsync(Guid menteeId, Guid goalId, MenteeGoalStatusUpdate statusUpdate, CancellationToken cancellationToken)
    {
        var menteeGoal = await _menteeGoalsRepository.GetByIdAsync(goalId, cancellationToken);
    
        if (menteeGoal is null || menteeGoal.MenteeId != menteeId)
        {
            throw new Exception("Mentee Goal not found or access denied.");
        }

        var newRequestStatus = (RequestStatus)statusUpdate.Status;

        if (newRequestStatus == RequestStatus.Approved)
        {
            var goalMaster = await _learningGoalsRepository.GetByIdAsync(menteeGoal.LearningId ?? Guid.Empty, cancellationToken);
            if (goalMaster is not null)
            {
                goalMaster.TargetDate = DateTime.Today.AddDays(14);

                goalMaster.Status = Status.NotStarted; 
                await _learningGoalsRepository.UpdateAsync(goalMaster);
            }
            else
            {
                throw new Exception("Learning Goal ID is missing on the mentee request.");
            }
            // goalMaster.TargetDate = DateTime.Today.AddDays(14);
        }

        menteeGoal.Status = newRequestStatus;

        await _unitOfWork.CommitTransactionAsync(async () =>
        {
            await _menteeGoalsRepository.UpdateAsync(menteeGoal);
        }, cancellationToken);
    }
}
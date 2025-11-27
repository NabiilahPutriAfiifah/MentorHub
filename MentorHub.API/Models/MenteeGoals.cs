using System.ComponentModel.DataAnnotations.Schema;

namespace MentorHub.API.Models;

public class MenteeGoals
{
    public Guid Id { get; set; }
    public Guid MenteeId { get; set; }
    public Guid LearningId { get; set; }

    // Cardinality
    public LearningGoals? LearningGoals { get; set; }
    public Accounts? Accounts { get; set; }


}

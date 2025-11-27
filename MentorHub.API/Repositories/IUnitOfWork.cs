namespace MentorHub.API.Repositories;

public interface IUnitOfWork
{
    Task CommitTransactionAsync(Func<Task> action, CancellationToken cancellationToken);
}

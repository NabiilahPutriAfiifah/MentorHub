using MentorHub.API.Data;

namespace MentorHub.API.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MentorHubDbContext _context;

    public UnitOfWork(MentorHubDbContext context)
    {
        _context = context;
    }

    public async Task CommitTransactionAsync(Func<Task> action, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await action();
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch 
        {
            await transaction.RollbackAsync(cancellationToken);
            throw; 
        }
    }
}

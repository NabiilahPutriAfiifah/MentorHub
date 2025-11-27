namespace MentorHub.API.Repositories.Interfaces;

public interface IRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<TEntity?> GetByIdAsync (Guid id, CancellationToken cancellationToken);
    Task CreateAsync(TEntity employee, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity employee);
    Task DeleteAsync(TEntity employee);
}

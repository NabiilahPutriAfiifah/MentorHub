using MentorHub.API.Data;
using MentorHub.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MentorHub.API.Repositories.Data;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly MentorHubDbContext _context;

    public Repository(MentorHubDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().FindAsync( id, cancellationToken);
    }

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }
}
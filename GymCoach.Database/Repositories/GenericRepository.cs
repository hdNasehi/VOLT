using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace GymCoach.Database.Repositories;

public sealed class GenericRepository<T>(GymCoachDbContext db) : IGenericRepository<T> where T : class
{
    private readonly DbSet<T> _set = db.Set<T>();

    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => _set.FindAsync([id], cancellationToken).AsTask();

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _set.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        => await _set.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _set.AddAsync(entity, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _set.Update(entity);
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _set.Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
    }

    public Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
        => predicate is null
            ? _set.CountAsync(cancellationToken)
            : _set.CountAsync(predicate, cancellationToken);
}

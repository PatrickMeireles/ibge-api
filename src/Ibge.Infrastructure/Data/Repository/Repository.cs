using Ibge.Domain.Entity;
using Ibge.Domain.Repository;
using Ibge.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ibge.Infrastructure.Data.Repository;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly DatabaseContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public Repository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public async Task Add(T entity, CancellationToken cancellationToken = default)
    {
        entity.SetCreatedAt(DateTimeOffset.Now);
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public async Task<PagedList<T>> Get(Expression<Func<T, bool>>? expression = null, int page = 0, int size = 0, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsNoTracking();

        var count = await query.CountAsync(cancellationToken);

        if (expression != null)
            query = query.Where(expression);

        if (page != default && size != default)
            query = query.Skip(Math.Abs((page - 1) * page)).Take(size);

        var items = await query.ToListAsync(cancellationToken);

        return new PagedList<T>(items, count, page, size);
    }

    public Task<IQueryable<T>> GetAll(int page = 0, int size = 0, CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsNoTracking();

        if (page != default && size != default)
            query = query.Skip(Math.Abs((page - 1) * page)).Take(size);

        return Task.FromResult(query);
    }

    public async Task<T?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task Remove(T entity, CancellationToken cancellationToken = default)
    {
        var result = await _dbSet.FirstOrDefaultAsync(c => c.Id == entity.Id, cancellationToken);

        if (result is not null)
            _dbSet.Remove(result);
    }

    public Task Update(T entity)
    {
        entity.SetUpdatedAt(DateTimeOffset.Now);
        _dbSet.Update(entity);

        return Task.CompletedTask;
    }
}

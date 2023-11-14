using Ibge.Domain.Constants;
using Ibge.Domain.Entity;
using Ibge.Domain.Repository;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace Ibge.Infrastructure.Data.MemoryCache;

public class CachedStateRepository : IStateRepository
{
    private readonly IStateRepository _decorated;
    private readonly IMemoryCache _memoryCache;

    private readonly MemoryCacheEntryOptions options;

    public CachedStateRepository(IStateRepository decorated, IMemoryCache memoryCache)
    {
        options = new()
        {
            AbsoluteExpiration = DateTime.UtcNow.AddMinutes(10)

        };
        _decorated = decorated;
        _memoryCache = memoryCache;
    }

    public async Task Add(State entity, CancellationToken cancellationToken = default)
    {
        await _decorated.Add(entity, cancellationToken);

        _memoryCache.Set($"{CacheKeyConstants.StateCache}-{entity.Id}", entity, options);
        _memoryCache.Set($"{CacheKeyConstants.StateCache}-{entity.Code}", entity.Id, options);
    }

    public async Task<PagedList<State>> Get(Expression<Func<State, bool>>? expression = null, int page = 0, int size = 0, CancellationToken cancellationToken = default) =>
        await _decorated.Get(expression, page, size, cancellationToken);

    public async Task<IQueryable<State>> GetAll(int page = 0, int size = 0, CancellationToken cancellationToken = default) =>
        await _decorated.GetAll(page, size, cancellationToken);

    public async Task<Guid?> GetIdByCode(int code, CancellationToken cancellationToken = default)
    {
        return await _memoryCache.GetOrCreateAsync($"{CacheKeyConstants.StateCache}-{code}", async c =>
        {
            var result = await _decorated.GetIdByCode(code, cancellationToken);
            c.SetOptions(options);
            return result;
        });
    }

    public async Task<State?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _memoryCache.GetOrCreateAsync($"{CacheKeyConstants.StateCache}-{id}", async c =>
        {
            var result = await _decorated.GetById(id, cancellationToken);

            if (result != null)
                _memoryCache.Set($"{CacheKeyConstants.StateCache}-{result.Code}", id, options);

            c.SetOptions(options);

            return result;
        });
    }

    public Task<State?> GetByIdWithCities(Guid id, CancellationToken cancellationToken = default) =>
        _decorated.GetByIdWithCities(id, cancellationToken);

    public async Task Remove(State entity, CancellationToken cancellationToken = default)
    {
        await _decorated.Remove(entity, cancellationToken);

        _memoryCache.Remove($"{CacheKeyConstants.StateCache}-{entity.Id}");
        _memoryCache.Remove($"{CacheKeyConstants.StateCache}-{entity.Code}");
    }

    public async Task Update(State entity)
    {
        await _decorated.Update(entity);

        _memoryCache.Set($"{CacheKeyConstants.StateCache}-{entity.Id}", entity, options);
        _memoryCache.Set($"{CacheKeyConstants.StateCache}-{entity.Code}", entity.Id, options);
    }
}

using Ibge.Domain.Entity;
using Ibge.Domain.Repository;
using Ibge.Infrastructure.Data.Context;
using Moq;
using System.Linq.Expressions;

namespace Ibge.Test.Extensions;

public static class RepositoryExpression<TRepository, TEntity> where TRepository : IRepository<TEntity> where TEntity : BaseEntity
{
    public readonly static Expression<Func<TRepository, Task<IQueryable<TEntity>>>> GetAll = c
        => c.GetAll(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>());

    public readonly static Expression<Func<TRepository, Task>> Add = c
        => c.Add(It.IsAny<TEntity>(), It.IsAny<CancellationToken>());

    public readonly static Expression<Func<TRepository, Task>> Update = c
       => c.Update(It.IsAny<TEntity>());

    public readonly static Expression<Func<TRepository, Task>> Remove = c
       => c.Remove(It.IsAny<TEntity>(), It.IsAny<CancellationToken>());

    public readonly static Expression<Func<TRepository, Task<TEntity?>>> GetById = c
       => c.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>());

    public readonly static Expression<Func<TRepository, Task<PagedList<TEntity>>>> GetPaged = c
        => c.Get(It.IsAny<Expression<Func<TEntity, bool>>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>());
}

public static class DatabaseContextExpression
{
    public readonly static Expression<Func<DatabaseContext, Task<bool>>> Commit = c => c.CommitAsync(It.IsAny<CancellationToken>());
}
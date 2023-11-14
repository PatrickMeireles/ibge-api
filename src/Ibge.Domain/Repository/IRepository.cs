using Ibge.Domain.Entity;
using System.Linq.Expressions;
namespace Ibge.Domain.Repository;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task Add(T entity, CancellationToken cancellationToken = default);
    Task Update(T entity);
    Task Remove(T entity, CancellationToken cancellationToken = default);
    Task<IQueryable<T>> GetAll(int page = default, int size = default, CancellationToken cancellationToken = default);
    Task<PagedList<T>> Get(Expression<Func<T, bool>>? expression = default, int page = default, int size = default, CancellationToken cancellationToken = default);
}

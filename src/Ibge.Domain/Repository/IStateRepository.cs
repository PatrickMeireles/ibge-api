using Ibge.Domain.Entity;
namespace Ibge.Domain.Repository;

public interface IStateRepository : IRepository<State>
{
    Task<Guid?> GetIdByCode(int code, CancellationToken cancellationToken = default);

    Task<State?> GetByIdWithCities(Guid id, CancellationToken cancellationToken = default);
}

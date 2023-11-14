namespace Ibge.Infrastructure.Data.Context;

public interface IContext
{
    Task<bool> CommitAsync(CancellationToken cancellationToken = default);
}

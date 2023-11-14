using Ibge.Infrastructure.Data.Context;

namespace Ibge.Application.Handler;

public abstract class CommandHandler
{
    private readonly IContext _context;

    protected CommandHandler(IContext context)
    {
        _context = context;
    }

    protected async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await _context.CommitAsync(cancellationToken);
    }
}

using Ibge.Domain.Entity;
using Ibge.Domain.Repository;
using Ibge.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Ibge.Infrastructure.Data.Repository;

public class StateRepository : Repository<State>, IStateRepository
{
    public StateRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }

    public async Task<Guid?> GetIdByCode(int code, CancellationToken cancellationToken = default)
    {
        var result = await _dbSet.Where(c => c.Code == code).FirstOrDefaultAsync(cancellationToken);

        return result?.Id;
    }

    public async Task<State?> GetByIdWithCities(Guid id, CancellationToken cancellationToken = default) =>
        await _dbSet.AsNoTracking().Where(c => c.Id == id).Include(c => c.Cities).FirstOrDefaultAsync(cancellationToken);
}

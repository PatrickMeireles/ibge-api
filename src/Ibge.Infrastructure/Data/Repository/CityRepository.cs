using Ibge.Domain.Entity;
using Ibge.Domain.Repository;
using Ibge.Infrastructure.Data.Context;

namespace Ibge.Infrastructure.Data.Repository;

public class CityRepository : Repository<City>, ICityRepository
{
    public CityRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }
}

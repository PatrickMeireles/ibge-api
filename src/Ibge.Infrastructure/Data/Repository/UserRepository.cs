using Ibge.Domain.Entity;
using Ibge.Domain.Repository;
using Ibge.Infrastructure.Data.Context;

namespace Ibge.Infrastructure.Data.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(DatabaseContext dbContext) : base(dbContext)
    {
    }
}

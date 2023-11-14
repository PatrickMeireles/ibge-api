using Ibge.Domain.Repository;
using Ibge.Infrastructure.Data.Repository;

namespace Ibge.Test.Mocks;

public interface IPersonRepository : IRepository<Person>
{

}
public class PersonRepository : Repository<Person>, IPersonRepository
{
    public PersonRepository(FakeContext dbContext) : base(dbContext)
    {
    }
}

using Ibge.Test.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace Ibge.Test.Infrastructure.Data.Repository;

[TestClass]
public class RepositoryTest
{

    private readonly PersonRepository _repository;
    private readonly Mock<FakeContext> _mockContext;
    private readonly List<Person> _people;
    private readonly Fixture _fixture;

    public RepositoryTest()
    {
        _fixture = new ();
        _people = new();

        _people = _fixture.CreateMany<Person>()
        .ToList();

        _mockContext = new();

        var mockDbSet = new Mock<DbSet<Person>>();

        var _mockDbSet = MockDatabaseSet.CreateDbSetMock(_people.AsQueryable());

        _mockContext.Setup(c => c.Set<Person>()).Returns(_mockDbSet.Object);

        _repository = new(_mockContext.Object);
    }

    [TestMethod]
    public async Task Should_Add_Return_Success()
    {
        var newPerson = new Person()
        {
            Name = "Teste"
        };

        await _repository.Add(newPerson, default);

        Assert.IsNotNull(newPerson);
        Assert.AreNotEqual(DateTime.MinValue, newPerson.CreatedAt);
    }

    [TestMethod]
    public async Task Should_Update_Return_Success()
    {
        var customer = _people.First();
        var updatedName = "updated name";

        await _repository.Update(customer);
        customer.Name = updatedName;

        var customers = await _repository.Get(x => x.Name.Equals(updatedName));

        Assert.IsNotNull(customers);
        Assert.IsNotNull(customer.UpdatedAt);
        Assert.AreEqual(1, customers.Count);
    }

    [TestMethod]
    public async Task Should_GetById_Return_Success()
    {
        var id = _people.First().Id;

        var c = await _repository.GetById(id);

        Assert.IsNotNull(c);
        Assert.AreEqual(c.Id, id);
    }

    [TestMethod]
    [DataRow(true, 1, 2)]
    [DataRow(false, default, default)]
    public async Task Should_Get_Return_Success(bool sendExpression, int page, int size)
    {
        Expression<Func<Person, bool>>? expression = c => c != null;

        var result = await _repository.Get(sendExpression ? expression : null, page, size, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Any());
    }

    [TestMethod]
    [DataRow(true, 1, 2)]
    [DataRow(false, default, default)]
    public async Task Should_GetPaged_Return_Success(bool sendExpression, int page, int size)
    {
        Expression<Func<Person, bool>>? expression = c => c != null;

        var result = await _repository.GetAll(page, size, default) ;

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Any());
    }


    [TestMethod]
    public async Task Should_Remove_Successfully()
    {
        var person = _people.First();

        await _repository.Remove(person, new());
        _people.Remove(person);

        var customers = await _repository.GetById(person.Id);

        Assert.IsNull(customers);
    }
}

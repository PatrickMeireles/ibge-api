using Ibge.Domain.Entity;
using Ibge.Infrastructure.Data.Context;
using Ibge.Infrastructure.Data.Repository;
using Ibge.Test.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ibge.Test.Infrastructure.Data.Repository;

[TestClass]
public class StateRepositoryTest
{
    private readonly Mock<DatabaseContext> _mockContext;
    private readonly Mock<DbSet<State>> _mockDbSet;
    private readonly StateRepository _stateRepository;
    private readonly Fixture _fixture;
    private readonly List<State> _states;
    public StateRepositoryTest()
    {
        _fixture = new();

        _mockContext = new ();

        _states = _fixture.CreateMany<State>().ToList();

        _mockDbSet = MockDatabaseSet.CreateDbSetMock(_states.AsQueryable());

        _mockContext.Setup(c => c.Set<State>()).Returns(_mockDbSet.Object);

        _stateRepository = new(_mockContext.Object);
    }

    [TestMethod]
    public void Should_Create_Instance() =>
        Assert.IsNotNull(_stateRepository);

    [TestMethod]
    public async Task Should_GetIdByCode_Return_Success()
    {
        var first = _states.First();

        var result = await _stateRepository.GetIdByCode(first.Code, default);

        Assert.IsNotNull(result);
        Assert.AreNotEqual(Guid.Empty, result);
    }

    [TestMethod]
    public async Task Should_GetIdByCode_Return_False()
    {
        var sumCode = _states.Sum(c => c.Code);

        var result = await _stateRepository.GetIdByCode(sumCode, default);

        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task Should_GetByIdWithCities_Return_Success()
    {
        var first = _states.First();

        var result = await _stateRepository.GetIdByCode(first.Code, default);

        Assert.IsNotNull(result);

    }
}

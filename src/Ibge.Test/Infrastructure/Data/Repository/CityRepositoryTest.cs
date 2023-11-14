using Ibge.Domain.Entity;
using Ibge.Infrastructure.Data.Context;
using Ibge.Infrastructure.Data.Repository;
using Ibge.Test.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ibge.Test.Infrastructure.Data.Repository;

[TestClass]
public class CityRepositoryTest
{
    private readonly Mock<DatabaseContext> _mockContext;
    private readonly Mock<DbSet<City>> _mockDbSet;
    private readonly CityRepository _cityRepository;
    public CityRepositoryTest()
    {
        _mockContext = new();

        var states = new List<City>();
        _mockDbSet = MockDatabaseSet.CreateDbSetMock(states.AsQueryable());

        _mockContext.Setup(c => c.Set<City>()).Returns(_mockDbSet.Object);

        _cityRepository = new(_mockContext.Object);
    }

    [TestMethod]
    public void Should_Create_Instance() =>
        Assert.IsNotNull(_cityRepository);
}

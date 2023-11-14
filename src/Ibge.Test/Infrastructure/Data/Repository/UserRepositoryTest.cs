using Ibge.Domain.Entity;
using Ibge.Infrastructure.Data.Context;
using Ibge.Infrastructure.Data.Repository;
using Ibge.Test.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ibge.Test.Infrastructure.Data.Repository;

[TestClass]
public class UserRepositoryTest
{
    private readonly Mock<DatabaseContext> _mockContext;
    private readonly Mock<DbSet<User>> _mockDbSet;
    private readonly UserRepository _userRepository;
    public UserRepositoryTest()
    {
        _mockContext = new();

        var states = new List<User>();
        _mockDbSet = MockDatabaseSet.CreateDbSetMock(states.AsQueryable());

        _mockContext.Setup(c => c.Set<User>()).Returns(_mockDbSet.Object);

        _userRepository = new(_mockContext.Object);
    }

    [TestMethod]
    public void Should_Create_Instance() =>
        Assert.IsNotNull(_userRepository);
}

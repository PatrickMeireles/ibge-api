using Ibge.Domain.Constants;
using Ibge.Domain.Entity;
using Ibge.Domain.Repository;
using Ibge.Infrastructure.Data.MemoryCache;
using Ibge.Test.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace Ibge.Test.Infrastructure.Data.MemoryCache;

[TestClass]
public class CachedStateRepositoryTest
{
    private readonly Mock<IStateRepository> _mockDecorated;
    private readonly Mock<IMemoryCache> _mockMemoryCache;
    private readonly CachedStateRepository _cachedStateRepository;
    private readonly Fixture _fixture;

    public CachedStateRepositoryTest()
    {
        _fixture = new();
        _mockDecorated = new();
        _mockMemoryCache = new();

        _mockMemoryCache
           .Setup(x => x.CreateEntry(It.IsAny<object>()))
           .Returns(Mock.Of<ICacheEntry>);
        _cachedStateRepository = new(_mockDecorated.Object, _mockMemoryCache.Object);
    }

    [TestMethod]
    public async Task Should_Add_State_Return_Success()
    {
        var entity = _fixture.Create<State>();

        await _cachedStateRepository.Add(entity, default);

        _mockDecorated.Verify(c => c.Add(It.IsAny<State>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_Get_States_Paged_Return_Success()
    {
        var response = _fixture.Create<PagedList<State>>();

        var expression = RepositoryExpression<IStateRepository, State>.GetPaged;

        _mockDecorated.Setup(expression).ReturnsAsync(response);

        var result = await _cachedStateRepository.Get(It.IsAny<Expression<Func<State, bool>>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>());

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Any());

        _mockDecorated.Verify(expression, Times.Once);
    }

    [TestMethod]
    public async Task Should_Get_States_Return_Success()
    {
        var response = _fixture.CreateMany<State>();

        var expression = RepositoryExpression<IStateRepository, State>.GetAll;

        _mockDecorated.Setup(expression).ReturnsAsync(response.AsQueryable());

        var result = await _cachedStateRepository.GetAll(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>());

        Assert.IsNotNull(result);
        Assert.IsTrue(result.Any());

        _mockDecorated.Verify(expression, Times.Once);
    }

    [TestMethod]
    public async Task Should_GetByCode_Return_Success()
    {
        var state = _fixture.Create<State>();

        _mockDecorated.Setup(c => c.GetIdByCode(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(state.Id);

        var result = await _cachedStateRepository.GetIdByCode(state.Code, default);

        Assert.IsNotNull(result);
        Assert.AreEqual(state.Id, result);

        _mockDecorated.Verify(c => c.GetIdByCode(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_GetById_Return_Success()
    {
        var state = _fixture.Create<State>();

        var expression = RepositoryExpression<IStateRepository, State>.GetById;

        _mockDecorated.Setup(expression).ReturnsAsync(state);

        var result = await _cachedStateRepository.GetById(state.Id, default);

        Assert.IsNotNull(result);
        Assert.AreEqual(state.Id, result.Id);
        Assert.AreEqual(state.Acronym, result.Acronym);
        Assert.AreEqual(state.Code, result.Code);
        Assert.AreEqual(state.Name, result.Name);

        _mockDecorated.Verify(c => c.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_GetByIdWithCities_Return_Success()
    {
        var state = _fixture.Create<State>();

        _mockDecorated.Setup(c => c.GetByIdWithCities(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(state);

        var result = await _cachedStateRepository.GetByIdWithCities(state.Id, default);

        Assert.IsNotNull(result);
        Assert.AreEqual(state.Id, result.Id);
        Assert.AreEqual(state.Acronym, result.Acronym);
        Assert.AreEqual(state.Code, result.Code);
        Assert.AreEqual(state.Name, result.Name);

        _mockDecorated.Verify(c => c.GetByIdWithCities(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_Remove_Return_Success()
    {
        var state = _fixture.Create<State>();

        var expression = RepositoryExpression<IStateRepository, State>.Remove;

        _mockDecorated.Setup(expression).Returns(Task.CompletedTask);

        await _cachedStateRepository.Remove(state);

        _mockDecorated.Verify(expression, Times.Once);


        _mockMemoryCache.Verify(c => c.Remove($"{CacheKeyConstants.StateCache}-{state.Id}"), Times.Once);
        _mockMemoryCache.Verify(c => c.Remove($"{CacheKeyConstants.StateCache}-{state.Code}"), Times.Once);
    }

    [TestMethod]
    public async Task Should_Update_Return_Success()
    {
        var state = _fixture.Create<State>();

        var expression = RepositoryExpression<IStateRepository, State>.Update;

        _mockDecorated.Setup(expression).Returns(Task.CompletedTask);

        await _cachedStateRepository.Update(state);

        _mockDecorated.Verify(expression, Times.Once);
    }
}

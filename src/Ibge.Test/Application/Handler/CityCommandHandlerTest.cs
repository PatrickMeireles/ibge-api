using Ibge.Application.Handler;
using Ibge.Domain.Command.City;
using Ibge.Domain.Entity;
using Ibge.Domain.Repository;
using Ibge.Infrastructure.Data.Context;
using Ibge.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ibge.Test.Application.Handler;

[TestClass]
public class CityCommandHandlerTest
{
    private readonly Mock<ICityRepository> _mockCityRepository;
    private readonly Mock<IStateRepository> _mockStateRepository;
    private readonly Mock<DatabaseContext> _mockContext;
    private readonly CityCommandHandler _handler;
    private readonly Fixture _fixture;
    public CityCommandHandlerTest()
    {
        _fixture = new();
        _mockCityRepository = new();
        _mockStateRepository = new();
        _mockContext = new();

        _handler = new(_mockContext.Object, _mockCityRepository.Object, _mockStateRepository.Object);
    }

    [TestMethod]
    public async Task Should_CityCreate_Handle_Return_Invalid()
    {
        var command = new CreateCityCommand()
        {
            Code = 0,
            Name = "A",
            StateId = Guid.Empty
        };

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task Should_CityCreate_Handle_When_NotFounded_State_Return_Invalid()
    {
        var command = new CreateCityCommand()
        {
            Code = 10,
            Name = "ABC",
            StateId = Guid.NewGuid()
        };

        var stateGetById = RepositoryExpression<IStateRepository, State>.GetById;

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);

        _mockStateRepository.Verify(stateGetById, Times.Once);
    }

    [TestMethod]
    public async Task Should_CityCreate_Handle_When_Conflicted_Return_Invalid()
    {
        var state = _fixture.Create<State>();

        var command = new CreateCityCommand()
        {
            Code = 10,
            Name = "ABC",
            StateId = state.Id
        };

        var city = new City(command.Code, command.Name, command.StateId);

        var cities = new List<City> { city };

        var stateGetById = RepositoryExpression<IStateRepository, State>.GetById;

        var getAll = RepositoryExpression<ICityRepository, City>.GetAll;

        _mockStateRepository.Setup(stateGetById).ReturnsAsync(state);

        _mockCityRepository.Setup(getAll).ReturnsAsync(cities.AsQueryable());

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);

        _mockStateRepository.Verify(stateGetById, Times.Once);
        _mockCityRepository.Verify(getAll, Times.Once);
    }

    [TestMethod]
    public async Task Should_CityCreate_Handle_Return_Success()
    {
        var state = _fixture.Create<Ibge.Domain.Entity.State>();

        var command = new CreateCityCommand()
        {
            Code = 10,
            Name = "ABC",
            StateId = state.Id
        };

        var city = new City(command.Code, command.Name, command.StateId);

        var cities = new List<City> { };

        var stateGetById = RepositoryExpression<IStateRepository, State>.GetById;

        var getAll = RepositoryExpression<ICityRepository, City>.GetAll;

        var add = RepositoryExpression<ICityRepository, City>.Add;

        _mockStateRepository.Setup(stateGetById).ReturnsAsync(state);

        _mockCityRepository.Setup(getAll).ReturnsAsync(cities.AsQueryable());

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);

        _mockStateRepository.Verify(stateGetById, Times.Once);
        _mockCityRepository.Verify(add, Times.Once);
    }

    [TestMethod]
    public async Task Should_CityUpdate_Handle_Return_Invalid()
    {
        var command = new UpdateCityCommand()
        {
            Code = 1,
            Name = "B",
            StateId = Guid.Empty
        };

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task Should_CityUpdate_Handle_When_NotFounded_State_Return_Invalid()
    {
        var command = new UpdateCityCommand()
        {
            Id = Guid.NewGuid(),
            Code = 10,
            Name = "ABC",
            StateId = Guid.NewGuid()
        };

        var stateGetById = RepositoryExpression<IStateRepository, State>.GetById;

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);

        _mockStateRepository.Verify(stateGetById, Times.Once);
    }

    [TestMethod]
    public async Task Should_CityUpdate_Handle_When_NotFound_City_Return_Invalid()
    {
        var state = _fixture.Create<State>();

        var command = new UpdateCityCommand()
        {
            Id = Guid.NewGuid(),
            Code = 10,
            Name = "ABC",
            StateId = Guid.NewGuid()
        };

        var stateGetById = RepositoryExpression<IStateRepository, State>.GetById;
        var getById = RepositoryExpression<ICityRepository, City>.GetById;

        _mockStateRepository.Setup(stateGetById).ReturnsAsync(state);

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);

        _mockStateRepository.Verify(stateGetById, Times.Once);
        _mockCityRepository.Verify(getById, Times.Once);
    }

    [TestMethod]
    public async Task Should_CityUpdate_Handle_When_Conflicted_Return_Invalid()
    {
        var state = _fixture.Create<State>();
        var fakeCity = _fixture.Create<City>();

        var command = new UpdateCityCommand()
        {
            Id = Guid.NewGuid(),
            Code = fakeCity.Code,
            Name = fakeCity.Name,
            StateId = fakeCity.StateId
        };

        var city = new City(command.Id, command.Code, command.Name, command.StateId);

        var cities = new List<City> { fakeCity };

        var stateGetById = RepositoryExpression<IStateRepository, State>.GetById;

        var getById = RepositoryExpression<ICityRepository, City>.GetById;

        var getAll = RepositoryExpression<ICityRepository, City>.GetAll;

        _mockStateRepository.Setup(stateGetById).ReturnsAsync(state);

        _mockCityRepository.Setup(getById).ReturnsAsync(city);

        _mockCityRepository.Setup(getAll).ReturnsAsync(cities.AsQueryable());

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);

        _mockStateRepository.Verify(stateGetById, Times.Once);
        _mockCityRepository.Verify(getById, Times.Once);
        _mockCityRepository.Verify(getAll, Times.Once);
    }


    [TestMethod]
    public async Task Should_CityUpdate_Handle_Return_Success()
    {
        var state = _fixture.Create<State>();

        var command = new UpdateCityCommand()
        {
            Id = Guid.NewGuid(),
            Code = 10,
            Name = "ABC",
            StateId = Guid.NewGuid()
        };

        var city = new City(command.Id, command.Code, command.Name, command.StateId);

        var cities = new List<City> { };

        var stateGetById = RepositoryExpression<IStateRepository, State>.GetById;

        var getById = RepositoryExpression<ICityRepository, City>.GetById;

        var getAll = RepositoryExpression<ICityRepository, City>.GetAll;

        _mockStateRepository.Setup(stateGetById).ReturnsAsync(state);

        _mockCityRepository.Setup(getById).ReturnsAsync(city);

        _mockCityRepository.Setup(getAll).ReturnsAsync(cities.AsQueryable());

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);

        _mockStateRepository.Verify(stateGetById, Times.Once);
        _mockCityRepository.Verify(getById, Times.Once);
        _mockCityRepository.Verify(getAll, Times.Once);
    }

    [TestMethod]
    public async Task Should_CityRemove_Return_Invalid()
    {
        var command = new RemoveCityCommand()
        {
            Id = Guid.Empty
        };

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task Should_CityRemove_When_NotFounded_Return_Invalid()
    {
        var command = new RemoveCityCommand()
        {
            Id = Guid.NewGuid()
        };

        var getById = RepositoryExpression<ICityRepository, City>.GetById;

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);

        _mockCityRepository.Verify(getById, Times.Once);
    }

    [TestMethod]
    public async Task Should_CityRemove_Return_Success()
    {
        var city = _fixture.Create<City>();

        var command = new RemoveCityCommand()
        {
            Id = city.Id
        };

        var getById = RepositoryExpression<ICityRepository, City>.GetById;

        _mockCityRepository.Setup(getById).ReturnsAsync(city);

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);

        _mockCityRepository.Verify(getById, Times.Once);

    }
}

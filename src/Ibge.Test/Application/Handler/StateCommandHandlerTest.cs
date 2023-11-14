using Ibge.Application.Handler;
using Ibge.Domain.Command.State;
using Ibge.Domain.Entity;
using Ibge.Domain.Repository;
using Ibge.Infrastructure.Data.Context;
using Ibge.Test.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ibge.Test.Application.Handler;

[TestClass]
public class StateCommandHandlerTest
{
    private readonly StateCommandHandler _handler;
    private readonly Mock<IStateRepository> _mockStateRepository;
    private readonly Mock<DatabaseContext> _mockContext;
    public StateCommandHandlerTest()
    {
        _mockStateRepository = new();
        _mockContext = new();

        _handler = new(_mockContext.Object, _mockStateRepository.Object);
    }

    [TestMethod]
    public async Task Should_CreateState_Handle_Return_Invalid()
    {
        var command = new CreateStateCommand()
        {
            Acronym = "A",
            Code = 0,
            Name = "AB"
        };

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task Should_CreateState_Handle_When_Already_Exist_Return_Invalid()
    {
        var command = new CreateStateCommand()
        {
            Acronym = "AB",
            Code = 10,
            Name = "ABcde"
        };

        var state = new State(command.Code, command.Name, command.Acronym);

        var states = new List<State>() { state };

        var getAllExpression = RepositoryExpression<IStateRepository, State>.GetAll;

        _mockStateRepository.Setup(getAllExpression).ReturnsAsync(states.AsQueryable());

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task Should_CreateState_Handle_Return_Success()
    {
        var command = new CreateStateCommand()
        {
            Acronym = "AB",
            Code = 10,
            Name = "ABcde"
        };

        var states = new List<State>() { };

        var getAllExpression = RepositoryExpression<IStateRepository, State>.GetAll;

        var addExpression = RepositoryExpression<IStateRepository, State>.Add;

        var commitExpression = DatabaseContextExpression.Commit;

        _mockStateRepository.Setup(getAllExpression).ReturnsAsync(states.AsQueryable());

        _mockContext.Setup(commitExpression).ReturnsAsync(true);

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);

        _mockStateRepository.Verify(getAllExpression, Times.Once);
        _mockStateRepository.Verify(addExpression, Times.Once);
        _mockContext.Verify(commitExpression, Times.Once);
    }

    [TestMethod]
    public async Task Should_UpdateState_Handle_Return_Invalid()
    {
        var command = new UpdateStateCommand()
        {
            Acronym = "B",
            Code = -1,
            Name = "BC",
            Id = Guid.Empty
        };

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task Should_UpdateState_Handle_When_NotFounded_Return_Invalid()
    {
        var command = new UpdateStateCommand()
        {
            Acronym = "BC",
            Code = 11,
            Name = "BCDE",
            Id = Guid.NewGuid()
        };

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task Should_UpdateState_Handle_When_Conflicted_Return_Invalid()
    {
        var command = new UpdateStateCommand()
        {
            Acronym = "BC",
            Code = 11,
            Name = "BCDE",
            Id = Guid.NewGuid()
        };

        var user = new State(command.Code, command.Name, command.Acronym);

        var states = new List<State>() { user };

        var getById = RepositoryExpression<IStateRepository, State>.GetById;

        var getAllExpression = RepositoryExpression<IStateRepository, State>.GetAll;

        _mockStateRepository.Setup(getById).ReturnsAsync(user);
        _mockStateRepository.Setup(getAllExpression).ReturnsAsync(states.AsQueryable());

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);

        _mockStateRepository.Verify(getAllExpression, Times.Once);
        _mockStateRepository.Verify(getById, Times.Once);
    }

    [TestMethod]
    public async Task Should_UpdateState_Handle_Return_Success()
    {
        var command = new UpdateStateCommand()
        {
            Acronym = "BC",
            Code = 11,
            Name = "BCDE",
            Id = Guid.NewGuid()
        };

        var user = new State(command.Code, command.Name, command.Acronym);

        var states = new List<State>() { };

        var getById = RepositoryExpression<IStateRepository, State>.GetById;

        var getAllExpression = RepositoryExpression<IStateRepository, State>.GetAll;

        var commitExpression = DatabaseContextExpression.Commit;

        var update = RepositoryExpression<IStateRepository, State>.Update;

        _mockContext.Setup(commitExpression).ReturnsAsync(true);

        _mockStateRepository.Setup(getById).ReturnsAsync(user);

        _mockStateRepository.Setup(getAllExpression).ReturnsAsync(states.AsQueryable());

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);

        _mockStateRepository.Verify(getAllExpression, Times.Once);
        _mockStateRepository.Verify(getById, Times.Once);
        _mockStateRepository.Verify(update, Times.Once);
        _mockContext.Verify(commitExpression, Times.Once);
    }

    [TestMethod]
    public async Task Should_RemoveState_Handle_Return_Invalid()
    {
        var command = new RemoveStateCommand()
        {
            Id = Guid.Empty
        };

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task Should_RemoveState_Handle_When_NotFounded_Return_Invalid()
    {
        var command = new RemoveStateCommand()
        {
            Id = Guid.NewGuid()
        };

        var getById = RepositoryExpression<IStateRepository, State>.GetById;

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);

        _mockStateRepository.Verify(c => c.GetByIdWithCities(command.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_RemoveState_Handle_When_Already_Cities_Liked_Return_Invalid()
    {
        var command = new RemoveStateCommand()
        {
            Id = Guid.NewGuid()
        };

        var city = new City(1000, "city", command.Id);

        var state = new State(command.Id, 10, "state", "st");

        state.AddCity(city);

        var getById = RepositoryExpression<IStateRepository, State>.GetById;

        _mockStateRepository.Setup(c => c.GetByIdWithCities(command.Id, It.IsAny<CancellationToken>())).ReturnsAsync(state);

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);

        _mockStateRepository.Verify(c => c.GetByIdWithCities(command.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_RemoveState_Handle_Return_Success()
    {
        var command = new RemoveStateCommand()
        {
            Id = Guid.NewGuid()
        };

        var state = new State(command.Id, 10, "state", "st");

        var getById = RepositoryExpression<IStateRepository, State>.GetById;

        var remove = RepositoryExpression<IStateRepository, State>.Remove;

        _mockStateRepository.Setup(c => c.GetByIdWithCities(command.Id, It.IsAny<CancellationToken>())).ReturnsAsync(state);

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);

        _mockStateRepository.Verify(c => c.GetByIdWithCities(command.Id, It.IsAny<CancellationToken>()), Times.Once);
        _mockStateRepository.Verify(remove, Times.Once);
    }
}

using Ardalis.Result;
using Ibge.Application.Services;
using Ibge.Domain.Command.State;
using Ibge.Domain.DTO.State;
using Ibge.Domain.Entity;
using Ibge.Domain.Repository;
using Ibge.Test.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ibge.Test.Application.Services;

[TestClass]
public class StateServicesTest
{
    private readonly Mock<IStateRepository> _mockStateRepository;
    private readonly Mock<ILogger<StateServices>> _mockLogger;
    private readonly Mock<IMediator> _mockMediator;
    private readonly StateServices _services;
    private readonly Fixture _fixture;

    public StateServicesTest()
    {
        _fixture = new();
        _mockLogger = new();
        _mockMediator = new();
        _mockStateRepository = new();

        _services = new(_mockStateRepository.Object, _mockMediator.Object, _mockLogger.Object);
    }

    [TestMethod]
    public async Task Should_GetById_Return_False()
    {
        var id = Guid.NewGuid();

        var getById = RepositoryExpression<IStateRepository, State>.GetById;

        var result = await _services.GetById(id, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);

        _mockStateRepository.Verify(getById, Times.Once);
    }

    [TestMethod]
    public async Task Should_GetById_Return_Success()
    {
        var State = _fixture.Create<State>();

        var getById = RepositoryExpression<IStateRepository, State>.GetById;

        _mockStateRepository.Setup(getById).ReturnsAsync(State);

        var result = await _services.GetById(State.Id, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);

        _mockStateRepository.Verify(getById, Times.Once);
    }

    [TestMethod]
    public async Task Should_GetIdByCode_Return_False()
    {
        var code = 1;

        var result = await _services.GetIdByCode(code, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);

        _mockStateRepository.Verify(c => c.GetIdByCode(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_GetIdByCode_Return_Success()
    {
        var State = _fixture.Create<State>();

        _mockStateRepository.Setup(c => c.GetIdByCode(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(Guid.NewGuid());

        var code = 1;

        var result = await _services.GetIdByCode(code, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);

        _mockStateRepository.Verify(c => c.GetIdByCode(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_Get_Return_Success()
    {
        var cities = _fixture.Create<PagedList<State>>();

        var param = new StateQueryParamsDto(Guid.Empty, null, "", null, 1, 20);

        var get = RepositoryExpression<IStateRepository, State>.GetPaged;

        _mockStateRepository.Setup(get).ReturnsAsync(cities);

        var result = await _services.Get(param, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);

        _mockStateRepository.Verify(get, Times.Once);
    }

    [TestMethod]
    public async Task Should_Send_Create_Command()
    {
        var command = _fixture.Create<CreateStateCommand>();

        var response = Result<Guid>.Success(Guid.NewGuid());

        _mockMediator.Setup(c => c.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(response);

        var result = await _services.Create(command, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);
    }


    [TestMethod]
    public async Task Should_Send_Update_Command()
    {
        var command = _fixture.Create<UpdateStateCommand>();

        var response = Result.Success();

        _mockMediator.Setup(c => c.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(response);

        var result = await _services.Update(command, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);
    }


    [TestMethod]
    public async Task Should_Send_Remove_Command()
    {
        var command = _fixture.Create<RemoveStateCommand>();

        var response = Result.Success();

        _mockMediator.Setup(c => c.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(response);

        var result = await _services.Remove(command, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);
    }

    [TestMethod]
    public async Task Should_AddFromFile_Return_Success()
    {
        var param = _fixture.Build<StateFromFileDto>()
                            .Create();

        var response = Result<Guid>.Success(Guid.NewGuid());

        _mockMediator.Setup(c => c.Send(It.IsAny<CreateStateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);

        var result = await _services.AddFromFile(param, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task Should_AddFromFile_Return_False()
    {
        var param = _fixture.Build<StateFromFileDto>()
                            .Create();

        var response = Result.Invalid(new());

        _mockMediator.Setup(c => c.Send(It.IsAny<CreateStateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);

        var result = await _services.AddFromFile(param, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result);
    }
}

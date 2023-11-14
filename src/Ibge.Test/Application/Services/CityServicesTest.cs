using Ardalis.Result;
using Ibge.Application.Services;
using Ibge.Domain.Command.City;
using Ibge.Domain.DTO.City;
using Ibge.Domain.Entity;
using Ibge.Domain.Repository;
using Ibge.Domain.Services;
using Ibge.Test.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ibge.Test.Application.Services;

[TestClass]
public class CityServicesTest
{
    private readonly Mock<ICityRepository> _mockCityRepository;
    private readonly Mock<IStateServices> _mockStateServices;
    private readonly Mock<ILogger<CityServices>> _mockLogger;
    private readonly Mock<IMediator> _mockMediator;
    private readonly CityServices _services;
    private readonly Fixture _fixture;

    public CityServicesTest()
    {
        _fixture = new();
        _mockCityRepository = new();
        _mockStateServices = new();
        _mockLogger = new();
        _mockMediator = new();

        _services = new(_mockCityRepository.Object, _mockStateServices.Object, _mockLogger.Object, _mockMediator.Object);
    }

    [TestMethod]
    public async Task Should_GetById_Return_False()
    {
        var id = Guid.NewGuid();

        var getById = RepositoryExpression<ICityRepository, City>.GetById;

        var result = await _services.GetById(id, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);

        _mockCityRepository.Verify(getById, Times.Once);
    }

    [TestMethod]
    public async Task Should_GetById_Return_Success()
    {
        var city = _fixture.Create<City>();

        var getById = RepositoryExpression<ICityRepository, City>.GetById;

        _mockCityRepository.Setup(getById).ReturnsAsync(city);

        var result = await _services.GetById(city.Id, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);

        _mockCityRepository.Verify(getById, Times.Once);
    }

    [TestMethod]
    public async Task Should_Get_Return_Success()
    {
        var cities = _fixture.Create<PagedList<City>>();

        var param = new CityQueryParamsDto(Guid.Empty, null, "", null, 1, 20);

        var get = RepositoryExpression<ICityRepository, City>.GetPaged;

        _mockCityRepository.Setup(get).ReturnsAsync(cities);

        var result = await _services.Get(param, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);

        _mockCityRepository.Verify(get, Times.Once);
    }

    [TestMethod]
    public async Task Should_AddFromFile_When_NotFound_State_Return_False()
    {
        var param = _fixture.Create<CityFromFileDto>();

        var response = Result.Invalid(new());

        _mockStateServices.Setup(c => c.GetIdByCode(param.StateCode, It.IsAny<CancellationToken>())).ReturnsAsync(response);

        var result = await _services.AddFromFile(param, default);


        Assert.IsNotNull(result);
        Assert.IsFalse(result);

        _mockStateServices.Verify(c => c.GetIdByCode(param.StateCode, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_AddFromFile_When_Command_Return_False()
    {

        var state = _fixture.Create<State>();

        var param = _fixture.Build<CityFromFileDto>()
                            .With(c => c.StateCode, state.Code)
                            .Create();

        _mockStateServices.Setup(c => c.GetIdByCode(param.StateCode, It.IsAny<CancellationToken>())).ReturnsAsync(state.Id);

        var response = Result<Guid>.Invalid(new());

        _mockMediator.Setup(c => c.Send(It.IsAny<CreateCityCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);

        var result = await _services.AddFromFile(param, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result);

        _mockStateServices.Verify(c => c.GetIdByCode(param.StateCode, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_AddFromFile_Return_Success()
    {

        var state = _fixture.Create<State>();

        var param = _fixture.Build<CityFromFileDto>()
                            .With(c => c.StateCode, state.Code)
                            .Create();

        _mockStateServices.Setup(c => c.GetIdByCode(param.StateCode, It.IsAny<CancellationToken>())).ReturnsAsync(state.Id);

        var response = Result<Guid>.Success(Guid.NewGuid());

        _mockMediator.Setup(c => c.Send(It.IsAny<CreateCityCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(response);

        var result = await _services.AddFromFile(param, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result);

        _mockStateServices.Verify(c => c.GetIdByCode(param.StateCode, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_Send_Create_Command()
    {
        var command = _fixture.Create<CreateCityCommand>();

        var response = Result<Guid>.Success(Guid.NewGuid());

        _mockMediator.Setup(c => c.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(response);

        var result = await _services.Create(command, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);
    }


    [TestMethod]
    public async Task Should_Send_Update_Command()
    {
        var command = _fixture.Create<UpdateCityCommand>();

        var response = Result.Success();

        _mockMediator.Setup(c => c.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(response);

        var result = await _services.Update(command, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);
    }


    [TestMethod]
    public async Task Should_Send_Remove_Command()
    {
        var command = _fixture.Create<RemoveCityCommand>();

        var response = Result.Success();

        _mockMediator.Setup(c => c.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(response);

        var result = await _services.Remove(command, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);
    }

}

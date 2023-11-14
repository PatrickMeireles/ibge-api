using Ardalis.Result;
using Ibge.Application.Services;
using Ibge.Domain.Command.User;
using Ibge.Domain.DTO;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ibge.Test.Application.Services;

[TestClass]
public class UserServicesTest
{
    private readonly Mock<IMediator> _mockMediator;
    private readonly UserServices _services;
    private readonly Fixture _fixture;

    public UserServicesTest()
    {
        _fixture = new();
        _mockMediator = new();
        _services = new(_mockMediator.Object);
    }

    [TestMethod]
    public async Task Should_Send_Auth_Command()
    {
        var command = _fixture.Create<AuthUserCommand>();

        var tokenResponse = new TokenResponse()
        {
            Token = "ABC"
        };

        var response = Result<TokenResponse>.Success(tokenResponse);

        _mockMediator.Setup(c => c.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(response);

        var result = await _services.Auth(command, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);
    }

    [TestMethod]
    public async Task Should_Send_Create_Command()
    {
        var command = _fixture.Create<CreateUserCommand>();

        var response = Result<Guid>.Success(Guid.NewGuid());

        _mockMediator.Setup(c => c.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(response);

        var result = await _services.Create(command, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);
    }
}

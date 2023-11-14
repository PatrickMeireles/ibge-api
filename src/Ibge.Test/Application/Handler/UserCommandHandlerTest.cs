using Ibge.Application.Configuration;
using Ibge.Application.Handler;
using Ibge.Application.UseCases;
using Ibge.Domain.Command.User;
using Ibge.Domain.Entity;
using Ibge.Domain.Repository;
using Ibge.Infrastructure.Data.Context;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq.Expressions;

namespace Ibge.Test.Application.Handler;

[TestClass]
public class UserCommandHandlerTest
{
    private readonly Fixture _fixture;
    private readonly Mock<IUserRepository> _mockRepository;
    private readonly Mock<GenerateTokenUseCase> _mockGenerateTokenUseCase;
    private readonly Mock<DatabaseContext> _mockDatabaseContext;
    private readonly UserCommandHandler _handler;

    public UserCommandHandlerTest()
    {
        _fixture = new();

        var fakeJWtOptions = _fixture.Create<JwtOptions>();

        var mockJwtOptions = Options.Create(fakeJWtOptions);
        _mockGenerateTokenUseCase = new(mockJwtOptions);

        _mockRepository = new();
        _mockDatabaseContext = new();


        _handler = new UserCommandHandler(_mockDatabaseContext.Object, _mockRepository.Object, _mockGenerateTokenUseCase.Object);
    }

    [TestMethod]
    public async Task Should_AuthUser_Handle_Return_Invalid()
    {
        var command = new AuthUserCommand()
        {
            Email = "teste",
            Password = "123"
        };

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);

        _mockRepository.Verify(repositoryExpressionGetAll, Times.Never);
    }

    [TestMethod]
    public async Task Should_AuthUser_Handle_When_NotFound_Return_Invalid()
    {
        var command = new AuthUserCommand()
        {
            Email = "teste@teste.com",
            Password = "123"
        };

        var mockResponse = new List<User>() { };

        _mockRepository.Setup(repositoryExpressionGetAll).ReturnsAsync(mockResponse.AsQueryable());

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);

        _mockRepository.Verify(repositoryExpressionGetAll, Times.Once);
    }

    [TestMethod]
    public async Task Should_AuthUser_Handle_When_InvalidPassword_Return_Invalid()
    {
        var command = new AuthUserCommand()
        {
            Email = "teste@teste.com",
            Password = "123"
        };

        var user = new User("teste", command.Email, BCrypt.Net.BCrypt.HashPassword(command.Password), false);

        var mockResponse = new List<User>() { user };

        _mockRepository.Setup(repositoryExpressionGetAll).ReturnsAsync(mockResponse.AsQueryable());

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);

        _mockRepository.Verify(repositoryExpressionGetAll, Times.Once);
    }

    [TestMethod]
    public async Task Should_CreateUser_Handle_Return_Invalid()
    {
        var command = new CreateUserCommand()
        {
            Email = "test",
            Name = "t",
            Password = "1"
        };

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);

        _mockRepository.Verify(repositoryExpressionGetAll, Times.Never);
    }

    [TestMethod]
    public async Task Should_CreateUSer_Handle_When_Already_With_Email_Return_False()
    {
        var command = new CreateUserCommand()
        {
            Email = "test@teste.com",
            Name = "teste",
            Password = "123"
        };

        var user = new User(command.Name, command.Email, command.Password, false);

        var users = new List<User>() { user };

        _mockRepository.Setup(repositoryExpressionGetAll).ReturnsAsync(users.AsQueryable());

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.IsSuccess);

        _mockRepository.Verify(repositoryExpressionGetAll, Times.Once);
    }

    [TestMethod]
    public async Task Should_CreateUser_Handle_Return_Success()
    {

        var command = new CreateUserCommand()
        {
            Email = "test@teste.com",
            Name = "teste",
            Password = "123"
        };

        var users = new List<User>() { };

        _mockRepository.Setup(repositoryExpressionGetAll).ReturnsAsync(users.AsQueryable());

        _mockDatabaseContext.Setup(c => c.CommitAsync(It.IsAny<CancellationToken>())).ReturnsAsync(true);

        var result = await _handler.Handle(command, default);

        Assert.IsNotNull(result);
        Assert.IsTrue(result.IsSuccess);

        _mockRepository.Verify(repositoryExpressionGetAll, Times.Once);
        _mockRepository.Verify(repositoryExpressionAdd, Times.Once);
    }

    private readonly static Expression<Func<IUserRepository, Task<IQueryable<User>>>> repositoryExpressionGetAll = c
        => c.GetAll(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>());

    private readonly static Expression<Func<IUserRepository, Task>> repositoryExpressionAdd = c
        => c.Add(It.IsAny<User>(), It.IsAny<CancellationToken>());
}

using Ardalis.Result;
using Ibge.Domain.Command.User;
using Ibge.Domain.Services;
using Ibge.Test.Api.Helper;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Ibge.Test.Api.Endpoints;

[TestClass]
public class UsersModuleTest
{

    private readonly TestServer _server;
    private readonly HttpClient _client;
    private readonly Mock<IUserServices> _mockStateServices;
    private readonly Fixture _fixture;

    public UsersModuleTest()
    {
        _fixture = new();
        _mockStateServices = new();

        var token = Guid.NewGuid().ToString();


        var hostBuilder = ModuleExtension.BuildHost();

        hostBuilder.ConfigureServices((services) =>
        {
            services.AddSingleton(_mockStateServices.Object);
        });

        IHost host = hostBuilder.Start();
        _server = host.GetTestServer();
        _client = _server.CreateClient();
    }

    [TestMethod]
    public async Task Should_Post_Return_Ok()
    {
        var model = _fixture.Create<CreateUserCommand>();

        _mockStateServices.Setup(c => c.Create(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success(Guid.NewGuid()));

        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await _client.PostAsync("api/v1/users", content);

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        _mockStateServices.Verify(c => c.Create(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_Auth_Return_Ok()
    {
        var model = _fixture.Create<AuthUserCommand>();

        _mockStateServices.Setup(c => c.Auth(It.IsAny<AuthUserCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success());

        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await _client.PostAsync("api/v1/users:auth", content);

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        _mockStateServices.Verify(c => c.Auth(It.IsAny<AuthUserCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

}

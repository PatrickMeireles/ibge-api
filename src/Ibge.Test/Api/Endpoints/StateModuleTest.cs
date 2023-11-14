using Ardalis.Result;
using Ibge.Api.Extension;
using Ibge.Domain.Command.City;
using Ibge.Domain.Command.State;
using Ibge.Domain.DTO.State;
using Ibge.Domain.Services;
using Ibge.Test.Api.Helper;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Ibge.Test.Api.Endpoints;

[TestClass]
public class StateModuleTest
{
    private readonly TestServer _server;
    private readonly HttpClient _client;
    private readonly Mock<IStateServices> _mockStateServices;
    private readonly Fixture _fixture;
    private readonly string _token;

    public StateModuleTest()
    {
        _fixture = new();
        _mockStateServices = new();

        var token = Guid.NewGuid().ToString();

        var tokenAndKey = ModuleExtension.GenerateFakeToken(token);

        _token = tokenAndKey;

        var configuration = ModuleExtension.ConfigurationRoot(token);
        var hostBuilder = ModuleExtension.BuildHost();

        hostBuilder.ConfigureServices((services) =>
        {
            services.AddSingleton(_mockStateServices.Object);
            services.ConfigureAddAuthentication(configuration);
        });

        IHost host = hostBuilder.Start();
        _server = host.GetTestServer();
        _client = _server.CreateClient();
    }

    [TestMethod]
    public async Task Should_Post_Return_Ok()
    {
        var model = _fixture.Create<CreateStateCommand>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        _mockStateServices.Setup(c => c.Create(It.IsAny<CreateStateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success(Guid.NewGuid()));

        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await _client.PostAsync("api/v1/states", content);

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        _mockStateServices.Verify(c => c.Create(It.IsAny<CreateStateCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_Get_Return_Ok()
    {
        _mockStateServices.Setup(c => c.Get(It.IsAny<StateQueryParamsDto>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success());

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var result = await _client.GetAsync("api/v1/states");

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        _mockStateServices.Verify(c => c.Get(It.IsAny<StateQueryParamsDto>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_GetById_Return_Ok()
    {
        var id = Guid.NewGuid();

        _mockStateServices.Setup(c => c.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success());

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var result = await _client.GetAsync($"api/v1/states/{id}");

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        _mockStateServices.Verify(c => c.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_Update_Return_Ok()
    {
        var model = _fixture.Create<UpdateStateCommand>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        _mockStateServices.Setup(c => c.Update(It.IsAny<UpdateStateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success());

        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await _client.PutAsync($"api/v1/states/{model.Id}", content);

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        _mockStateServices.Verify(c => c.Update(It.IsAny<UpdateStateCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }


    [TestMethod]
    public async Task Should_Update_Return_Conflict()
    {
        var model = _fixture.Create<UpdateStateCommand>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await _client.PutAsync($"api/v1/states/{Guid.NewGuid()}", content);

        Assert.AreEqual(HttpStatusCode.Conflict, result.StatusCode);

        _mockStateServices.Verify(c => c.Update(It.IsAny<UpdateStateCommand>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod]
    public async Task Should_Remove_Return_Ok()
    {
        var model = _fixture.Create<RemoveStateCommand>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        _mockStateServices.Setup(c => c.Remove(It.IsAny<RemoveStateCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success());

        var result = await _client.DeleteAsync($"api/v1/states/{model.Id}");

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        _mockStateServices.Verify(c => c.Remove(It.IsAny<RemoveStateCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        // Limpar recursos após os testes
        _client.Dispose();
        _server.Dispose();
    }
}

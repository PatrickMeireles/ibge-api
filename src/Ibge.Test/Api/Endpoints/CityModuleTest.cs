using Ardalis.Result;
using Ibge.Api.Extension;
using Ibge.Application;
using Ibge.Application.Configuration;
using Ibge.Application.UseCases;
using Ibge.Domain.Command.City;
using Ibge.Domain.DTO.City;
using Ibge.Domain.Entity;
using Ibge.Domain.Services;
using Ibge.Test.Api.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Ibge.Test.Api.Endpoints;

[TestClass]
public class CityModuleTest
{
    private readonly TestServer _server;
    private readonly HttpClient _client;
    private readonly Mock<ICityServices> _mockCityServices;
    private readonly Fixture _fixture;
    private readonly string _token;

    public CityModuleTest()
    {
        _fixture = new();
        _mockCityServices = new();

        var token = Guid.NewGuid().ToString();

        var tokenAndKey = ModuleExtension.GenerateFakeToken(token);

        _token = tokenAndKey;

        var configuration = ModuleExtension.ConfigurationRoot(token);
        var hostBuilder = ModuleExtension.BuildHost();

        hostBuilder.ConfigureServices((services) =>
        {
            services.AddSingleton(_mockCityServices.Object);
            services.ConfigureAddAuthentication(configuration);
        });

        IHost host = hostBuilder.Start();
        _server = host.GetTestServer();
        _client = _server.CreateClient();
    }

    [TestMethod]
    public async Task Should_Post_Return_Ok()
    {
        var model = _fixture.Create<CreateCityCommand>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        _mockCityServices.Setup(c => c.Create(It.IsAny<CreateCityCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success(Guid.NewGuid()));

        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await _client.PostAsync("api/v1/cities", content);
                
        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        _mockCityServices.Verify(c => c.Create(It.IsAny<CreateCityCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_Get_Return_Ok()
    {
        _mockCityServices.Setup(c => c.Get(It.IsAny<CityQueryParamsDto>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success());

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var result = await _client.GetAsync("api/v1/cities");

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        _mockCityServices.Verify(c => c.Get(It.IsAny<CityQueryParamsDto>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_GetById_Return_Ok()
    {
        var id = Guid.NewGuid();

        _mockCityServices.Setup(c => c.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success());

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var result = await _client.GetAsync($"api/v1/cities/{id}");

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        _mockCityServices.Verify(c => c.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_Update_Return_Ok()
    {
        var model = _fixture.Create<UpdateCityCommand>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        _mockCityServices.Setup(c => c.Update(It.IsAny<UpdateCityCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success());

        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await _client.PutAsync($"api/v1/cities/{model.Id}", content);

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        _mockCityServices.Verify(c => c.Update(It.IsAny<UpdateCityCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_Update_Return_Conflict()
    {
        var model = _fixture.Create<UpdateCityCommand>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var json = JsonSerializer.Serialize(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var result = await _client.PutAsync($"api/v1/cities/{Guid.NewGuid()}", content);

        Assert.AreEqual(HttpStatusCode.Conflict, result.StatusCode);

        _mockCityServices.Verify(c => c.Update(It.IsAny<UpdateCityCommand>(), It.IsAny<CancellationToken>()), Times.Never);
    }


    [TestMethod]
    public async Task Should_Remove_Return_Ok()
    {
        var model = _fixture.Create<RemoveCityCommand>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        _mockCityServices.Setup(c => c.Remove(It.IsAny<RemoveCityCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Result.Success());

        var result = await _client.DeleteAsync($"api/v1/cities/{model.Id}");

        Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        _mockCityServices.Verify(c => c.Remove(It.IsAny<RemoveCityCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        // Limpar recursos após os testes
        _client.Dispose();
        _server.Dispose();
    }
}
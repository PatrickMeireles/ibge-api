using Ibge.Api.Extension;
using Ibge.Domain.Services;
using Ibge.Test.Api.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Ibge.Test.Api.Endpoints;

[TestClass]
public class ImportModuleTest
{
    private readonly TestServer _server;
    private readonly HttpClient _client;
    private readonly Mock<IImportServices> _mockImportServices;
    private readonly string _token;

    public ImportModuleTest()
    {
        _mockImportServices = new();

        var token = Guid.NewGuid().ToString();

        var tokenAndKey = ModuleExtension.GenerateFakeToken(token);

        _token = tokenAndKey;

        var configuration = ModuleExtension.ConfigurationRoot(token);
        var hostBuilder = ModuleExtension.BuildHost();

        hostBuilder.ConfigureServices((services) =>
        {
            services.AddSingleton(_mockImportServices.Object);
            services.ConfigureAddAuthentication(configuration);
        });

        IHost host = hostBuilder.Start();
        _server = host.GetTestServer();
        _client = _server.CreateClient();
    }

    [TestMethod]
    public async Task Should_Import_Return_Ok()
    {
        var file = CreateTestFile("file", "file.xlsx", "ABC");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        _mockImportServices.Setup(c => c.ProccessFile(It.IsAny<Guid>(), It.IsAny<IFormFile>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var result = await _client.PostAsync("api/v1/import-xlsx", file);

        Assert.AreEqual(HttpStatusCode.Accepted, result.StatusCode);

        _mockImportServices.Verify(c => c.ProccessFile(It.IsAny<Guid>(), It.IsAny<IFormFile>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Should_Import_When_Not_Exist_File_Return_BadRequest()
    {
        var file = CreateTestFile("files", "file.xlsx", "ABC");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var result = await _client.PostAsync("api/v1/import-xlsx", file);

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);

        _mockImportServices.Verify(c => c.ProccessFile(It.IsAny<Guid>(), It.IsAny<IFormFile>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod]
    public async Task Should_Import_When_Empty_File_Return_BadRequest()
    {
        var file = CreateTestFile("files", "file.xlsx", "");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var result = await _client.PostAsync("api/v1/import-xlsx", file);

        Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);

        _mockImportServices.Verify(c => c.ProccessFile(It.IsAny<Guid>(), It.IsAny<IFormFile>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod]
    public async Task Should_Import_When_Invalid_Format_Return_UnsupportedMediaType()
    {
        var file = CreateTestFile("file", "file.pdf", "ABC");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        var result = await _client.PostAsync("api/v1/import-xlsx", file);

        Assert.AreEqual(HttpStatusCode.UnsupportedMediaType, result.StatusCode);

        _mockImportServices.Verify(c => c.ProccessFile(It.IsAny<Guid>(), It.IsAny<IFormFile>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    public static MultipartFormDataContent CreateTestFile(string headerName, string fileName, string fileContent)
    {
        var content = new MultipartFormDataContent
        {
            { new ByteArrayContent(Encoding.UTF8.GetBytes(fileContent)), headerName , fileName }
        };
        return content;
    }
}

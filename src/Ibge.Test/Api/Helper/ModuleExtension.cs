using Ibge.Application.Configuration;
using Ibge.Application.UseCases;
using Ibge.Domain.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Ibge.Test.Api.Helper;

public static class ModuleExtension
{
    public static IHostBuilder BuildHost()
    {
        IHostBuilder hostBuilder = new HostBuilder()
              .ConfigureWebHost(webHost =>
              {
                  webHost.UseStartup<CustomStartup>();
                  webHost.UseTestServer();
              });

        return hostBuilder;
    }

    public static IConfigurationRoot ConfigurationRoot(string key)
    {
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
                new Dictionary<string, string>
                {
                    ["JwtOptions:Key"] = key,
                    ["JwtOptions:TimeToExpiresInMinutes"] = "10"
                }).Build();

        return configuration;
    }

    public static string GenerateFakeToken(string key)
    {
        var user = new User("teste", "teste@teste.com", "md91hj892hejv", true);

        var JwtOPtion = new JwtOptions()
        {
            Key = key,
            TimeToExpiresInMinutes = 1
        };

        var mockJwtOptions = Options.Create(JwtOPtion);

        var _generateTokenUseCase = new GenerateTokenUseCase(mockJwtOptions);

        return _generateTokenUseCase.Action(user);
    }
}

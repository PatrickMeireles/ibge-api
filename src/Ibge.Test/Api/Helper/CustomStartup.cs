using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ibge.Test.Api.Helper;

public class CustomStartup
{
    public IConfiguration Configuration { get; }

    public CustomStartup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddRouting();
        services.AddAuthorization();
        services.AddControllers();
        services.AddCarter();        
    }

    public static void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHttpsRedirection();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapCarter();
        });
    }
}

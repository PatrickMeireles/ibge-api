using Ibge.Domain.DTO.City;
using Ibge.Domain.DTO.State;
using Ibge.Domain.Repository;
using Ibge.Infrastructure.Data.Context;
using Ibge.Infrastructure.Data.MemoryCache;
using Ibge.Infrastructure.Data.Repository;
using Ibge.Infrastructure.Services;
using Ibge.Infrastructure.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ibge.Infrastructure;

public static class Configure
{
    public static IServiceCollection ConfigureInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<StateFromFileServices>();
        services.AddHostedService<CityFromFileServices>();

        return services
            .ConfigureEntityFramework(configuration)
            .ConfigureRepositories()
            .ConfigureWorkers();
    }

    private static IServiceCollection ConfigureEntityFramework(this IServiceCollection services, IConfiguration configuration)
    {
        var stringConnection = configuration.GetConnectionString("Database");

        ArgumentNullException.ThrowIfNull(stringConnection);

        var options = new DbContextOptionsBuilder<DatabaseContext>();
        options.UseSqlite(stringConnection);
        options.LogTo(Console.Write, Microsoft.Extensions.Logging.LogLevel.Warning);
        _ = new DatabaseContext(options.Options, true);

        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseSqlite(stringConnection);
            options.LogTo(Console.Write, Microsoft.Extensions.Logging.LogLevel.Warning);
        });

        services.AddHealthChecks()
                .AddDbContextCheck<DatabaseContext>("sqlite");

        return services;
    }

    private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IStateRepository, StateRepository>()
                .Decorate<IStateRepository, CachedStateRepository>();

        services.AddScoped<ICityRepository, CityRepository>();

        return services;
    }

    private static IServiceCollection ConfigureWorkers(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IChannelService<StateFromFileDto>), typeof(ChannelService<StateFromFileDto>));
        services.AddSingleton(typeof(IChannelService<CityFromFileDto>), typeof(ChannelService<CityFromFileDto>));

        return services;
    }
}

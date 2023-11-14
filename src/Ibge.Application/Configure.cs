using Ardalis.Result;
using FluentValidation;
using Ibge.Application.Configuration;
using Ibge.Application.Handler;
using Ibge.Application.Services;
using Ibge.Application.UseCases;
using Ibge.Application.Validators.City;
using Ibge.Application.Validators.State;
using Ibge.Application.Validators.User;
using Ibge.Domain.Command.City;
using Ibge.Domain.Command.State;
using Ibge.Domain.Command.User;
using Ibge.Domain.DTO;
using Ibge.Domain.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ibge.Application;

public static class Configure
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<GenerateTokenUseCase>();
        services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.ConfigureValidators()
                .ConfigureHandlers()
                .ConfigureServices()
                .AddMemoryCache();

        return services;
    }

    public static IServiceCollection ConfigureValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateUserCommand>, CreateUserValidator>();
        services.AddScoped<IValidator<AuthUserCommand>, AuthUserValidator>();

        services.AddScoped<IValidator<CreateStateCommand>, CreateStateValidator>();
        services.AddScoped<IValidator<UpdateStateCommand>, UpdateStateValidator>();
        services.AddScoped<IValidator<RemoveStateCommand>, RemoveStateValidator>();

        services.AddScoped<IValidator<CreateCityCommand>, CreateCityValidator>();
        services.AddScoped<IValidator<UpdateCityCommand>, UpdateCityValidator>();

        return services;
    }

    public static IServiceCollection ConfigureHandlers(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CreateUserCommand, Result<Guid>>, UserCommandHandler>();
        services.AddScoped<IRequestHandler<AuthUserCommand, Result<TokenResponse>>, UserCommandHandler>();

        services.AddScoped<IRequestHandler<CreateStateCommand, Result<Guid>>, StateCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateStateCommand, Result>, StateCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveStateCommand, Result>, StateCommandHandler>();

        services.AddScoped<IRequestHandler<CreateCityCommand, Result<Guid>>, CityCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateCityCommand, Result>, CityCommandHandler>();
        services.AddScoped<IRequestHandler<RemoveCityCommand, Result>, CityCommandHandler>();

        return services;
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IStateServices, StateServices>();
        services.AddScoped<ICityServices, CityServices>();
        services.AddScoped<IImportServices, ImportServices>();
        services.AddScoped<IUserServices, UserServices>();

        return services;
    }
}

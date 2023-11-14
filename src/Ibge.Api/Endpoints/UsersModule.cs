using Ardalis.Result.AspNetCore;
using Carter;
using Ibge.Domain.Command.User;
using Ibge.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ibge.Api.Endpoints;

public class UsersModule : ICarterModule
{
    private readonly string Tag = "User";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/users", async (CreateUserCommand command,
            [FromServices] IUserServices _services,
            CancellationToken cancellationToken) =>
        {
            var result = await _services.Create(command, cancellationToken);

            return result.ToMinimalApiResult();
        })
            .Produces(200)
            .Produces(400)
            .WithTags(Tag)
            .AllowAnonymous();

        app.MapPost("api/v1/users:auth", async (AuthUserCommand command,
            [FromServices] IUserServices _services,
            CancellationToken cancellationToken) =>
        {
            var result = await _services.Auth(command, cancellationToken);

            return result.ToMinimalApiResult();
        })
            .Produces(200)
            .Produces(400)
            .WithTags(Tag)
            .AllowAnonymous();
    }
}

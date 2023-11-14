using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Carter;
using Ibge.Domain.Command.State;
using Ibge.Domain.DTO.State;
using Ibge.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ibge.Api.Endpoints;

public class StatesModule : ICarterModule
{
    private readonly string Tag = "State";

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/states", async (CreateStateCommand model,
            [FromServices] IStateServices _services,
            CancellationToken cancellationToken) =>
        {
            var result = await _services.Create(model, cancellationToken);

            return result.ToMinimalApiResult();
        })
            .Produces(200)
            .Produces(400)
            .Produces(403)
            .WithTags(Tag);

        app.MapGet("api/v1/states/{id}", async (Guid id,
            [FromServices] IStateServices _services,
            CancellationToken cancellationToken) =>
        {
            var result = await _services.GetById(id, cancellationToken);

            return result.ToMinimalApiResult();
        })
            .Produces<StateResponseDto>(200)
            .Produces(404)
            .WithTags(Tag);

        app.MapGet("api/v1/states", async (Guid? id,
            int? code,
            string? name,
            string? acronym,
            int? page,
            int? size,
            [FromServices] IStateServices _services,
            CancellationToken cancellationToken) =>
        {
            var result = await _services.Get(new(id, code, name, acronym, page ?? 1, size ?? 20), cancellationToken);

            return result.ToMinimalApiResult();
        })
            .Produces<PagedResult<IEnumerable<StateResponseDto>>>(200)
            .WithTags(Tag);

        app.MapPut("api/v1/states/{id}", async (Guid id,
            UpdateStateCommand model,
            [FromServices] IStateServices _services,
            CancellationToken cancellationToken) =>
        {
            if (id != model.Id)
                return Results.Conflict();

            var result = await _services.Update(model, cancellationToken);

            return result.ToMinimalApiResult();

        })
            .Produces(200)
            .Produces(400)
            .Produces(404)
            .WithTags(Tag);

        app.MapDelete("api/v1/states/{id}", async (Guid id,
            [FromServices] IStateServices _services,
            CancellationToken cancellationToken) =>
        {
            var model = new RemoveStateCommand()
            {
                Id = id
            };

            await _services.Remove(model, cancellationToken);

            return Results.Ok();
        })
            .Produces(200)
            .Produces(400)
            .Produces(404)
            .WithTags(Tag);
    }
}
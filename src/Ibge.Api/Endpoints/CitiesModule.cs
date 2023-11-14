using Ardalis.Result.AspNetCore;
using Carter;
using Ibge.Domain.Command.City;
using Ibge.Domain.DTO;
using Ibge.Domain.DTO.City;
using Ibge.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ibge.Api.Endpoints;

public class CitiesModule : ICarterModule
{
    private readonly string Tag = "City";
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/cities", async (CreateCityCommand model,
            [FromServices] ICityServices _services,
            CancellationToken cancellationToken) =>
        {
            var result = await _services.Create(model, cancellationToken);

            return result.ToMinimalApiResult();
        })
            .Produces(200)
            .Produces(400)
            .Produces(403)
            .WithTags(Tag)
            .RequireAuthorization();

        app.MapGet("api/v1/cities/{id}", async (Guid id,
            [FromServices] ICityServices _cityServices,
            CancellationToken cancellationToken) =>
        {
            var result = await _cityServices.GetById(id, cancellationToken);

            return result.ToMinimalApiResult();
        })
            .Produces<CityResponseDto>(200)
            .Produces(404)
            .WithTags(Tag)
            .RequireAuthorization();

        app.MapGet("api/v1/cities", async (Guid? id,
            int? code,
            string? name,
            Guid? stateId,
            int? page,
            int? size,
            [FromServices] ICityServices _cityServices,
            CancellationToken cancellationToken) =>
        {
            var result = await _cityServices.Get(new(id, code, name, stateId, page ?? 1, size ?? 20), cancellationToken);

            return result.ToMinimalApiResult();
        })
            .Produces<PagedResponseDto<CityResponseDto>>(200)
            .WithTags(Tag)
            .RequireAuthorization();


        app.MapPut("api/v1/cities/{id}", async (Guid id,
            UpdateCityCommand model,
            [FromServices] ICityServices _services,
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
            .WithTags(Tag)
            .RequireAuthorization();

        app.MapDelete("api/v1/cities/{id}", async (Guid id,
            [FromServices] ICityServices _services,
            CancellationToken cancellationToken) =>
        {
            var model = new RemoveCityCommand()
            {
                Id = id
            };

            var result = await _services.Remove(model, cancellationToken);

            return result.ToMinimalApiResult();
        })
            .Produces(200)
            .Produces(400)
            .Produces(404)
            .WithTags(Tag)
            .RequireAuthorization();
    }
}

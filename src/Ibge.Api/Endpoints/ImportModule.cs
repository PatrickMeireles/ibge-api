using Carter;
using Ibge.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ibge.Api.Endpoints;

public class ImportModule : ICarterModule
{
    private readonly string Tag = "Import";
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/v1/import-xlsx", async (HttpContext context,
           [FromServices] IImportServices services,
           CancellationToken cancellationToken) =>
        {
            var id = Guid.NewGuid();
            var formFile = context.Request.Form.Files["file"];

            if (formFile is not null && formFile.Length > 0)
            {
                if (!formFile.FileName.Contains(".xlsx"))
                    return Results.StatusCode(415);

                await services.ProccessFile(id, formFile, cancellationToken);

                var result = new
                {
                    Id = id,
                    Message = "File Accepted"
                };

                return Results.Accepted("", result);
            }
            return Results.BadRequest("Format File is not valid.");

        })
           .Produces(202)
           .Produces(400)
           .Produces(403)
           .WithTags(Tag)
           .RequireAuthorization();
    }
}

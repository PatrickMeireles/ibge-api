using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation;

namespace Ibge.Application.Extensions;

public static class FluentValidationExtension<T, Instance> where T : IValidator<Instance> where Instance : class
{
    public async static Task<List<ValidationError>> GetValidationErrors(Instance request, CancellationToken cancellationToken)
    {
        var validator = Activator.CreateInstance<T>();

        var validation = await validator.ValidateAsync(request, cancellationToken);

        if (!validation.IsValid)
            return validation.AsErrors();

        return new();
    }
}

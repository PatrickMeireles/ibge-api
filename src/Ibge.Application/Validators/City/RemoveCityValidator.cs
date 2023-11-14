using FluentValidation;
using Ibge.Domain.Command.City;

namespace Ibge.Application.Validators.City;

public class RemoveCityValidator : AbstractValidator<RemoveCityCommand>
{
    public RemoveCityValidator()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty);
    }
}
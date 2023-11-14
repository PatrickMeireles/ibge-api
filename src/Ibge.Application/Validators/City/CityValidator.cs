using FluentValidation;
using Ibge.Domain.Command.City;

namespace Ibge.Application.Validators.City;

public abstract class CityValidator<T> : AbstractValidator<T> where T : CityCommand
{
    public void ValidateName()
    {
        RuleFor(c => c.Name)
           .NotEmpty()
           .NotNull()
           .MinimumLength(3)
           .MaximumLength(255);
    }

    public void ValidateCode()
    {
        RuleFor(c => c.Code)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);
    }

    public void ValidateStateId()
    {
        RuleFor(c => c.StateId)
           .NotEmpty()
           .NotNull()
           .NotEqual(Guid.Empty);
    }
}

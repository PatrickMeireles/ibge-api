using FluentValidation;
using Ibge.Domain.Command.State;

namespace Ibge.Application.Validators.State;

public abstract class StateValidator<T> : AbstractValidator<T> where T : StateCommand
{
    protected void ValidateCode()
    {
        RuleFor(c => c.Code)
            .NotEmpty()
            .NotNull()
            .GreaterThan(0);
    }

    protected void ValidateName()
    {
        RuleFor(c => c.Name)
           .NotEmpty()
           .NotNull()
           .MinimumLength(3)
           .MaximumLength(255);
    }

    protected void ValidateAcronym()
    {
        RuleFor(c => c.Acronym)
           .NotEmpty()
           .NotNull()
           .Must(c => c.Length == 2)
           .WithMessage("Acronym must have 2 characters.");
    }
}

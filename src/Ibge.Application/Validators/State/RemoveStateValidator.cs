using FluentValidation;
using Ibge.Domain.Command.State;

namespace Ibge.Application.Validators.State;

public class RemoveStateValidator : AbstractValidator<RemoveStateCommand>
{
    public RemoveStateValidator()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty);
    }
}

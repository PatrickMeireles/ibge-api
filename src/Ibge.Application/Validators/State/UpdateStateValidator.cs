using FluentValidation;
using Ibge.Domain.Command.State;

namespace Ibge.Application.Validators.State;

public class UpdateStateValidator : StateValidator<UpdateStateCommand>
{
    public UpdateStateValidator()
    {
        ValidateCode();
        ValidateName();
        ValidateAcronym();
        ValidateId();
    }

    protected void ValidateId()
    {
        RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
    }
}

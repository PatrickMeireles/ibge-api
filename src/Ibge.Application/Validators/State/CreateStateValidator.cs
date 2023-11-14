using Ibge.Domain.Command.State;

namespace Ibge.Application.Validators.State;

public class CreateStateValidator : StateValidator<CreateStateCommand>
{
    public CreateStateValidator()
    {
        ValidateCode();
        ValidateName();
        ValidateAcronym();
    }
}

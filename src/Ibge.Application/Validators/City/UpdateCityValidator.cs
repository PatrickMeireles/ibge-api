using FluentValidation;
using Ibge.Domain.Command.City;

namespace Ibge.Application.Validators.City;

public class UpdateCityValidator : CityValidator<UpdateCityCommand>
{
    public UpdateCityValidator()
    {
        ValidateCode();
        ValidateName();
        ValidateStateId();

        RuleFor(c => c.Id)
          .NotEmpty()
          .NotNull()
          .NotEqual(Guid.Empty);
    }
}

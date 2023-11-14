using Ibge.Domain.Command.City;

namespace Ibge.Application.Validators.City;

public class CreateCityValidator : CityValidator<CreateCityCommand>
{
    public CreateCityValidator()
    {
        ValidateCode();
        ValidateName();
        ValidateStateId();
    }
}

using Ardalis.Result;
using MediatR;

namespace Ibge.Domain.Command.City;

public class CreateCityCommand : CityCommand, IRequest<Result<Guid>>
{
}

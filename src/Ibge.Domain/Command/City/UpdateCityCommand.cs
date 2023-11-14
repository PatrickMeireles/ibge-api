using Ardalis.Result;
using MediatR;

namespace Ibge.Domain.Command.City;

public class UpdateCityCommand : CityCommand, IRequest<Result>
{
    public Guid Id { get; set; }
}

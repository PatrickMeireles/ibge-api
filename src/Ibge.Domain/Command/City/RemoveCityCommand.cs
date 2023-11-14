using Ardalis.Result;
using MediatR;

namespace Ibge.Domain.Command.City;

public class RemoveCityCommand : IRequest<Result>
{
    public Guid Id { get; set; }
}

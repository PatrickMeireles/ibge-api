using Ardalis.Result;
using MediatR;

namespace Ibge.Domain.Command.State;

public class RemoveStateCommand : IRequest<Result>
{
    public Guid Id { get; set; }
}

using Ardalis.Result;
using MediatR;

namespace Ibge.Domain.Command.State;

public class UpdateStateCommand : StateCommand, IRequest<Result>
{
    public Guid Id { get; set; }
}

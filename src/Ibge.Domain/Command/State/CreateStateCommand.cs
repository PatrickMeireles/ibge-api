using Ardalis.Result;
using MediatR;

namespace Ibge.Domain.Command.State;

public class CreateStateCommand : StateCommand, IRequest<Result<Guid>>
{

}

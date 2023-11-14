using Ardalis.Result;
using MediatR;

namespace Ibge.Domain.Command.User;

public class CreateUserCommand : UserCommand, IRequest<Result<Guid>>
{
    public string Name { get; set; } = string.Empty;

    public bool IsAdmin { get; set; }
}

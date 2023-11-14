using Ardalis.Result;
using Ibge.Domain.DTO;
using MediatR;

namespace Ibge.Domain.Command.User;

public class AuthUserCommand : UserCommand, IRequest<Result<TokenResponse>>
{
}

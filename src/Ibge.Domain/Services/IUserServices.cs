using Ardalis.Result;
using Ibge.Domain.Command.User;
using Ibge.Domain.DTO;

namespace Ibge.Domain.Services;

public interface IUserServices
{
    Task<Result<TokenResponse>> Auth(AuthUserCommand request, CancellationToken cancellationToken);
    Task<Result<Guid>> Create(CreateUserCommand request, CancellationToken cancellationToken);
}

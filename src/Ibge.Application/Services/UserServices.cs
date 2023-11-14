using Ardalis.Result;
using Ibge.Domain.Command.User;
using Ibge.Domain.DTO;
using Ibge.Domain.Services;
using MediatR;

namespace Ibge.Application.Services;

public class UserServices : IUserServices
{
    private readonly IMediator _mediator;

    public UserServices(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<Result<TokenResponse>> Auth(AuthUserCommand request, CancellationToken cancellationToken) =>
        await _mediator.Send(request, cancellationToken);

    public async Task<Result<Guid>> Create(CreateUserCommand request, CancellationToken cancellationToken) =>
        await _mediator.Send(request, cancellationToken);

}

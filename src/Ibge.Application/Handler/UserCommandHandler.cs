using Ardalis.Result;
using Ibge.Application.Extensions;
using Ibge.Application.UseCases;
using Ibge.Application.Validators.User;
using Ibge.Domain.Adapter;
using Ibge.Domain.Command;
using Ibge.Domain.Command.User;
using Ibge.Domain.DTO;
using Ibge.Domain.Repository;
using Ibge.Infrastructure.Data.Context;
using MediatR;
using Bcrypt = BCrypt.Net.BCrypt;

namespace Ibge.Application.Handler;

public class UserCommandHandler : CommandHandler,
        IRequestHandler<AuthUserCommand, Result<TokenResponse>>,
        IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IUserRepository _repository;
    private readonly GenerateTokenUseCase _generateTokenUseCase;

    public UserCommandHandler(DatabaseContext context, IUserRepository repository, GenerateTokenUseCase generateTokenUseCase) : base(context)
    {
        _repository = repository;
        _generateTokenUseCase = generateTokenUseCase;
    }

    public async Task<Result<TokenResponse>> Handle(AuthUserCommand request, CancellationToken cancellationToken)
    {
        var errors = await FluentValidationExtension<AuthUserValidator, AuthUserCommand>.GetValidationErrors(request, cancellationToken);

        if (errors.Any())
            return Result.Invalid(errors);

        var query = (await _repository.GetAll(cancellationToken: cancellationToken)).Where(c => c.Email == request.Email);

        var user = query.FirstOrDefault();

        if (user == null || !Bcrypt.Verify(request.Password, user.Password))
            return Result.Invalid(ValidationErrorExtension.AddError("Authentication", "Invalid Credentials"));

        var tokenResponse = new TokenResponse
        {
            Token = _generateTokenUseCase.Action(user)
        };

        return Result.Success(tokenResponse);
    }

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var errors = await FluentValidationExtension<CreateUserValidator, CreateUserCommand>.GetValidationErrors(request, cancellationToken);

        if (errors.Any())
            return Result.Invalid(errors);

        var exist = (await _repository.GetAll(cancellationToken: cancellationToken)).Where(c => c.Email == request.Email);

        if (exist.Any())
            return Result.Invalid(ValidationErrorExtension.AddError("Conflict", "Already exist User with this same parameters"));

        var user = UserAdapter.CreateNewUser(request);

        await _repository.Add(user, cancellationToken);

        await CommitAsync(cancellationToken);

        return Result.Success(user.Id);
    }
}

using Ibge.Domain.Command.User;

namespace Ibge.Application.Validators.User;

public class AuthUserValidator : UserValidator<AuthUserCommand>
{
    public AuthUserValidator()
    {
        ValidEmail();
        ValidPassword();
    }
}

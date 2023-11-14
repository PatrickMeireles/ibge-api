using FluentValidation;
using Ibge.Domain.Command.User;

namespace Ibge.Application.Validators.User;

public class CreateUserValidator : UserValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        ValidEmail();
        ValidPassword();

        RuleFor(c => c.Name)
           .NotEmpty()
           .NotNull()
           .MinimumLength(3)
           .MaximumLength(255);

    }
}

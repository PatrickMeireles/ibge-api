using FluentValidation;
using Ibge.Domain.Command.User;

namespace Ibge.Application.Validators.User
{
    public class UserValidator<T> : AbstractValidator<T> where T : UserCommand
    {
        public void ValidEmail()
        {
            RuleFor(c => c.Email)
                 .NotEmpty()
                 .NotNull()
                 .EmailAddress()
                 .MaximumLength(255);
        }

        public void ValidPassword()
        {
            RuleFor(c => c.Password)
            .NotEmpty()
            .NotNull();
        }
    }
}

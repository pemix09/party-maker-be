using Application.User.Commands;
using FluentValidation;

namespace Application.User.Validators
{
    public class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email cannot be empty!");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password field cannot be empty!");
        }
    }
}

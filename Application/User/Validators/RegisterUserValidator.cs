using Application.User.Commands;
using FluentValidation;

namespace Application.User.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email for new user have to be provided!");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password for new user have to be provided!")
                .MinimumLength(8)
                .WithMessage("Password must have at least 8 characters!");
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Username cannot be empty!");
        }
    }
}

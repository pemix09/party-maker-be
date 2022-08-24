using Application.Ban.Commands;
using FluentValidation;

namespace Application.Ban.Validators
{
    public class DeleteBanValidator : AbstractValidator<DeleteBanCommand>
    {
        public DeleteBanValidator()
        {
            RuleFor(x => x.BanId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ban's id is not valid");
        }
    }
}

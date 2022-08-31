using Application.Ban.Commands;
using FluentValidation;

namespace Application.Ban.Validators
{
    public class CreateBanValidator : AbstractValidator<CreateBanCommand>
    {
        public CreateBanValidator()
        {
            RuleFor(x => x.Reason)
                .NotEmpty()
                .WithMessage("Reason cannot be empty");
            RuleFor(x => x.End)
                .GreaterThan(DateTime.Now)
                .WithMessage("Ending date must be bigger than now!");
            RuleFor(x => x.BannedUserId)
                .NotEmpty()
                .WithMessage("Banned user have to be specified!");
        }
    }
}

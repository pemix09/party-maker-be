using Application.Ban.Commands;
using FluentValidation;

namespace Application.Ban.Validators
{
    public class UpdateBanValidator : AbstractValidator<UpdateBanCommand>
    {
        public UpdateBanValidator()
        {
            RuleFor(x => x.NewEndDate)
                .GreaterThan(DateTime.Now)
                .WithMessage("New end date have to be bigger than now!");
            RuleFor(x => x.BanId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Wong Ban id!");

        }
    }
}

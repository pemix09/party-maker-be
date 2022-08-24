using Application.Ban.Queries;
using FluentValidation;

namespace Application.Ban.Validators
{
    public class GetBanByIdValidator : AbstractValidator<GetBanByIdQuery>
    {
        public GetBanByIdValidator()
        {
            RuleFor(x => x.BanID)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ban id not correct!");
        }
    }
}

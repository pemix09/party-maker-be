using Application.EntrancePass.Queries;
using FluentValidation;

namespace Application.EntrancePass.Validators
{
    public class GetEntrancePassByIdValidator : AbstractValidator<GetEntrancePassByIdQuery>
    {
        public GetEntrancePassByIdValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Id is invalid!");
        }
    }
}

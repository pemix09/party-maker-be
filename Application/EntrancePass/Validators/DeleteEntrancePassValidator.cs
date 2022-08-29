using Application.EntrancePass.Commands;
using FluentValidation;

namespace Application.EntrancePass.Validators
{
    public class DeleteEntrancePassValidator : AbstractValidator<DeleteEntrancePassCommand>
    {
        public DeleteEntrancePassValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Id is invalid!");
        }
    }
}

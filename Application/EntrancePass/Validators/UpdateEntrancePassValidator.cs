using Application.EntrancePass.Commands;
using FluentValidation;

namespace Application.EntrancePass.Validators
{
    public class UpdateEntrancePassValidator : AbstractValidator<UpdateEntrancePassCommand>
    {
        public UpdateEntrancePassValidator()
        {
            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Price cannot be empty!");
        }
    }
}

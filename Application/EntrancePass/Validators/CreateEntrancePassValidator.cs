using Application.EntrancePass.Commands;
using FluentValidation;

namespace Application.EntrancePass.Validators
{
    public class CreateEntrancePassValidator : AbstractValidator<CreateEntrancePassCommand>
    {
        public CreateEntrancePassValidator()
        {
            RuleFor(x => x.PassType)
                .NotEmpty()
                .WithMessage("Pass type have to be provided!");
        }
    }
}

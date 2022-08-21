using Application.Message.Commands;
using FluentValidation;

namespace Application.Message.Validators
{
    public class DeleteMessageValidator : AbstractValidator<DeleteMessageCommand>
    {
        public DeleteMessageValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id for message to delete have to be provided!");
        }
    }
}

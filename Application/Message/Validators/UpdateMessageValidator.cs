using Application.Message.Commands;
using FluentValidation;

namespace Application.Message.Validators
{
    public class UpdateMessageValidator : AbstractValidator<UpdateMessageCommand>
    {
        public UpdateMessageValidator()
        {
            RuleFor(x => x.NewContent)
                .NotEmpty()
                .WithMessage("Nothing was changed in message!");
            RuleFor(x => x.MessageId)
                .NotEmpty()
                .WithMessage("No message Id was given!");
        }
    }
}

using Application.Message.Commands;
using FluentValidation;

namespace Application.Message.Validators
{
    public class CreateMessageValidator : AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Message content cannot be empy!");
            RuleFor(x => x.SenderId)
                .NotEmpty()
                .WithMessage("Sender Id cannot be empty!");
            RuleFor(x => x.ReceiverId)
                .NotEmpty()
                .WithMessage("Receiver Id cannot be empty!");
            RuleFor(x => x.EventId)
                .NotEmpty()
                .WithMessage("Event Id cannot be empty!");
        }
    }
}

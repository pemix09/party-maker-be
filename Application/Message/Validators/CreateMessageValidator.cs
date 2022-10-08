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
                .WithMessage("Message content cannot be empty!");
            RuleFor(x => x.ReceiverId)
                .NotEmpty()
                .WithMessage("Receiver Id cannot be empty!");
            RuleFor(x => x.EventId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Invalid event Id!");
        }
    }
}

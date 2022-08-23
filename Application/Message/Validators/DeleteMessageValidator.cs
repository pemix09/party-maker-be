using Application.Message.Commands;
using FluentValidation;

namespace Application.Message.Validators
{
    public class DeleteMessageValidator : AbstractValidator<DeleteMessageCommand>
    {
        public DeleteMessageValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("Id for message to delete have to be provided!")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Message Id have to bigger or equal to 0!");
        }
    }
}

using Application.Message.Queries;
using FluentValidation;

namespace Application.Message.Validators
{
    public class GetMessageByIdValidator : AbstractValidator<GetMessageByIdQuery>
    {
        public GetMessageByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Message id cannot be empty!");
        }
    }
}

using Application.Event.Commands;

namespace Application.Event.Validators;
using FluentValidation;

public class CreateEventValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description cannot be empty!");
    }
    
}
using Application.Message.Commands;

namespace Application.Message.Validators;
using FluentValidation;

public class CreateEventValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description cannot be empty!");
        RuleFor(x => x.Longitude)
            .NotEmpty()
            .WithMessage("Longitude has to be provided!");
        RuleFor(x => x.Latitude)
            .NotEmpty()
            .WithMessage("Latitude has to be provided!");
    }
    
}
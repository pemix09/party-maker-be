

namespace Application.Message.Commands;
using MediatR;
using Application.Message.Validators;
using FluentValidation;
using Persistence.UnitOfWork;
using Core.Models;
using Persistence.Services.Database;

public class CreateEventCommand : IRequest<Unit>
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string Place { get; init; }
    public int PassId { get; init; }
    public string Photo { get; init; }
    public int MusicGenreId { get; set; }
    public string Type { get; set; }

    public class Handler : IRequestHandler<CreateEventCommand, Unit>
    {
        private readonly EventService eventService;
        private readonly UserService userService;
        public Handler(EventService _eventService, UserService _userService)
        {
            eventService = _eventService;
            userService = _userService;
        }
        public async Task<Unit> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            await new CreateEventValidator().ValidateAndThrowAsync(request, cancellationToken);

            AppUser organizer = await userService.GetCurrentlySignedIn();
            
            Event _event = Event.Create(
                request.Description,
                request.Place,
                organizer.Id,
                request.PassId,
                request.Photo,
                request.MusicGenreId,
                request.Type);

            await eventService.AddToDataBase(_event);

            return Unit.Value;
        }
    }


}
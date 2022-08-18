

namespace Application.Event.Commands;
using MediatR;
using Application.Event.Validators;
using FluentValidation;
using Persistence.UnitOfWork;
using Core.Models;
using Persistence.Services.Database;

public class CreateEventCommand : IRequest<Unit>
{
    public string Name { get; init; }
    public string Description { get; init; }
    public string Place { get; init; }
    public int OrganizerId { get; init; }
    public int PassId { get; init; }
    public string Photo { get; init; }
    public int MusicGenreId { get; set; }

    public class Handler : IRequestHandler<CreateEventCommand, Unit>
    {
        private readonly EventService eventService;
            
        public Handler(IUnitOfWork unitOfWork)
        {
            eventService = new EventService(unitOfWork);
        }
        public async Task<Unit> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            await new CreateEventValidator().ValidateAndThrowAsync(request, cancellationToken);
            
            Event _event = Event.Create(
                request.Description,
                request.Place,
                request.OrganizerId,
                request.PassId,
                request.Photo,
                request.MusicGenreId);

            await eventService.Create(_event);

            return Unit.Value;
        }
    }


}
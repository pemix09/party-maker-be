namespace Application.Event.Commands;
using MediatR;
using Core.Models;
using Persistence.Services.Database;

public class UpdateEventCommand : IRequest<Unit>
{
    public Event Edited { get; init; }

    public class Handler : IRequestHandler<UpdateEventCommand, Unit>
    {
        private readonly EventService eventService;
        public Handler(EventService _eventService)
        {
            eventService = _eventService;
        }

        public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            await eventService.UpdateInDataBase(request.Edited);

            return Unit.Value;
        }
    }

}
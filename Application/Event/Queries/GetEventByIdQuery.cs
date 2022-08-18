namespace Application.Event.Queries;
using Persistence.UnitOfWork;
using MediatR;
using Core.Models;
using Persistence.Services.Database;

public class GetEventByIdQuery : IRequest<Event>
{
    public int EventId { get; init; }

    public class Handler : IRequestHandler<GetEventByIdQuery, Event>
    {
        private readonly EventService eventService;
        public Handler(EventService _eventService)
        {
            eventService = _eventService;
        }
        public async Task<Event> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await eventService.GetByIdFromDataBase(request.EventId);
            
            return entity;
        }
    }
}
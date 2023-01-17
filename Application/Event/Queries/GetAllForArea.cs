using MediatR;
using Persistence.Services.Database;

namespace Application.Event.Queries
{
    using Core.Models;
    public class GetAllForArea : IRequest<IEnumerable<Event>>
    {
        public double latNorth { get; init; }
        public double latSouth { get; init; }
        public double lonEast { get; init; }
        public double lonWest { get; init; }
        public class Handler : IRequestHandler<GetAllForArea, IEnumerable<Event>>
        {
            private readonly EventService eventService;
            public Handler(EventService _eventService)
            {
                eventService = _eventService;
            }

            public async Task<IEnumerable<Event>> Handle(GetAllForArea request, CancellationToken cancellationToken)
            {
                IEnumerable<Event> events = eventService.GetForArea(request.latNorth, request.latSouth, request.lonEast, request.lonWest);

                return events;
            }
        }
    }
}

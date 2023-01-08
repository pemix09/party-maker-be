using MediatR;
using Persistence.Services.Database;

namespace Application.Event.Queries
{
    using Core.Models;
    public class GetAllForAreaByQuery : IRequest<IEnumerable<Event>>
    {
        public string Query { get; init; }
        public double latNorth { get; init; }
        public double latSouth { get; init; }
        public double lonEast { get; init; }
        public double lonWest { get; init; }
        public class Handler : IRequestHandler<GetAllForAreaByQuery, IEnumerable<Event>>
        {
            private readonly EventService eventService;
            public Handler(EventService _eventService)
            {
                eventService = _eventService;
            }

            public async Task<IEnumerable<Event>> Handle(GetAllForAreaByQuery request, CancellationToken cancellationToken)
            {
                IEnumerable<Event> events = await eventService.GetAllFromDataBase();

                return events;
            }
        }
    }
}

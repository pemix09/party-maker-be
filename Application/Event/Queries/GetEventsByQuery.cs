namespace Application.Event.Queries
{
    using Application.Message.Queries;
    using Core.Models;
    using MediatR;
    using Persistence.Services.Database;
    using System.Threading.Tasks;

    public class GetEventsByQuery : IRequest<IEnumerable<Event>>
    {
        public string? Query { get; set; }
        public class Handler : IRequestHandler<GetEventsByQuery, IEnumerable<Event>>
        {
            private readonly EventService eventService;
            public Handler(EventService _eventService)
            {
                eventService = _eventService;
            }
            public async Task<IEnumerable<Event>> Handle(GetEventsByQuery request, CancellationToken cancellationToken)
            {
                if(request.Query != null)
                {
                    var events = eventService.GetByQuery(request.Query);
                    return await Task.FromResult(events);
                }
                return Enumerable.Empty<Event>();
            }

        }
    }
}

namespace Application.Event.Queries
{
    using Core.Models;
    using MediatR;
    using Persistence.Services.Database;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetEventsByNameQuery : IRequest<IEnumerable<Event>>
    {
        public string Name { get; set; }
        public class Handler : IRequestHandler<GetEventsByNameQuery, IEnumerable<Event>>
        {
            private readonly EventService eventService;
            public Handler(EventService _eventService)
            {
                eventService = _eventService;
            }
            public async Task<IEnumerable<Event>> Handle(GetEventsByNameQuery request, CancellationToken cancellationToken)
            {
                return await Task.Run(() => eventService.GetByQuery(request.Name));
            }
        }
    }
}

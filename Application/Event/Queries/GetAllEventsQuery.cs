using Persistence.UnitOfWork;

namespace Application.Event.Queries;
using MediatR;
using Core.Models;
using Persistence.Services.Database;

public class GetAllEventsQuery : IRequest<IEnumerable<Event>>
{
    public class Handler : IRequestHandler<GetAllEventsQuery, IEnumerable<Event>>
    {
        private readonly EventService eventService;
        public Handler(IUnitOfWork unitOfWork)
        {
            eventService = new EventService(unitOfWork);
        }

        public async Task<IEnumerable<Event>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Event> events = await eventService.GetAllFromDataBase();

            return events;
        }
    }
}
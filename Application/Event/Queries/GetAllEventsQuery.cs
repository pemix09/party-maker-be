using Persistence.UnitOfWork;

namespace Application.Event.Queries;
using MediatR;
using Core.Models;

public class GetAllEventsQuery : IRequest<IEnumerable<Event>>
{
    public class Handler : IRequestHandler<GetAllEventsQuery, IEnumerable<Event>>
    {
        private readonly IUnitOfWork UnitOfWork;
            
        public Handler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        
        public async Task<IEnumerable<Event>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Event> events = await  UnitOfWork.Events.GetAll();

            return events;
        }
    }
}
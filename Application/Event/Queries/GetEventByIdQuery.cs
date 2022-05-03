namespace Application.Event.Queries;
using Persistence.UnitOfWork;
using MediatR;
using Core.Models;

public class GetEventByIdQuery : IRequest<Event>
{
    public int EventId { get; init; }

    public class Handler : IRequestHandler<GetEventByIdQuery, Event>
    {
        private readonly IUnitOfWork UnitOfWork;
            
        public Handler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        
        public async Task<Event> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await UnitOfWork.Events.Get(request.EventId);
            
            return entity;
        }
    }
}
namespace Application.Event.Commands;
using MediatR;
using Persistence.UnitOfWork;
using Core.Models;
using Persistence.Services.Database;

public class DeleteEventCommand : IRequest<Unit>
{
    public int EventId { get; init; }

    public class Handler : IRequestHandler<DeleteEventCommand, Unit>
    {
        private readonly EventService eventService;
        public Handler(IUnitOfWork unitOfWork)
        {
            eventService = new EventService(unitOfWork);
        }
        
        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            await eventService.DeleteFromDataBase(request.EventId);

            return Unit.Value;
        }
    }
}
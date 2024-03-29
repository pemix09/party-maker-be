namespace Application.Message.Commands;
using MediatR;
using Persistence.UnitOfWork;
using Persistence.Services.Database;

public class DeleteEventCommand : IRequest<Unit>
{
    public int EventId { get; init; }

    public class Handler : IRequestHandler<DeleteEventCommand, Unit>
    {
        private readonly EventService eventService;
        public Handler(EventService _eventService)
        {
            eventService = _eventService;
        }

        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            await eventService.DeleteFromDataBase(request.EventId);

            return Unit.Value;
        }
    }
}
namespace Application.Event.Commands;
using MediatR;
using Persistence.UnitOfWork;
using Core.Models;
using Persistence.Services.Database;

public class UpdateEventCommand : IRequest<Unit>
{
    public Event Edited { get; init; }

    public class Handler : IRequestHandler<UpdateEventCommand, Unit>
    {
        private readonly EventService eventService;
        public Handler(IUnitOfWork unitOfWork)
        {
            eventService = new EventService(unitOfWork);
        }

        public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            await eventService.UpdateInDataBase(request.Edited);

            return Unit.Value;
        }
    }

}
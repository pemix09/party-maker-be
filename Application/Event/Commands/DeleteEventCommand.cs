namespace Application.Event.Commands;
using MediatR;
using Persistence.UnitOfWork;
using Core.Models;

public class DeleteEventCommand : IRequest<Unit>
{
    public int EventId { get; init; }

    public class Handler : IRequestHandler<DeleteEventCommand, Unit>
    {
        private readonly IUnitOfWork UnitOfWork;
            
        public Handler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        
        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            Event entity = await UnitOfWork.Events.Get(request.EventId);
            UnitOfWork.Events.Remove(entity);
            await UnitOfWork.Complete();

            return Unit.Value;
        }
    }
}
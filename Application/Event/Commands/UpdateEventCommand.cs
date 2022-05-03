namespace Application.Event.Commands;
using MediatR;
using Persistence.UnitOfWork;
using Core.Models;

public class UpdateEventCommand : IRequest<Unit>
{
    public Event Edited { get; init; }

    public class Handler : IRequestHandler<UpdateEventCommand, Unit>
    {
        private readonly IUnitOfWork UnitOfWork;
            
        public Handler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        
        public async Task<Unit> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            UnitOfWork.Events.Update(request.Edited);
            await UnitOfWork.Complete();

            return Unit.Value;
        }
    }

}
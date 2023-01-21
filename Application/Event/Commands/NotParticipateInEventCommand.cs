using MediatR;
using Persistence.Services.Database;

namespace Application.Event.Commands
{
    public class NotParticipateInEventCommand : IRequest<Unit>
    {
        public int EventToNotParticipate { get; init; }

        public class Handler : IRequestHandler<NotParticipateInEventCommand, Unit>
        {
            private readonly EventService eventService;
            private readonly IUserService userService;
            public Handler(EventService _eventService, IUserService _userService)
            {
                eventService = _eventService;
                userService = _userService;
            }

            public async Task<Unit> Handle(NotParticipateInEventCommand request, CancellationToken cancellationToken)
            {
                await userService.NotParticipateInEvent(request.EventToNotParticipate);

                return Unit.Value;
            }
        }
    }
}

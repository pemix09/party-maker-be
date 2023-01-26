using MediatR;
using Persistence.Services.Database;
using Persistence.Exceptions;

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
                var user = await userService.GetCurrentlySignedIn();
                if(user == null)
                {
                    throw new UserNotAuthenticatedException();
                }
                await userService.NotParticipateInEvent(request.EventToNotParticipate);
                await eventService.NotParticipateInEvent(user.Id, request.EventToNotParticipate);

                return Unit.Value;
            }
        }
    }
}

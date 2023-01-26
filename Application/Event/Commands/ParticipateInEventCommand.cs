using MediatR;
using Persistence.Services.Database;

namespace Application.Event.Commands
{
    public class ParticipateInEventCommand : IRequest<Unit>
    {
        public int EventToParticipate { get; init; }

        public class Handler : IRequestHandler<ParticipateInEventCommand, Unit>
        {
            private readonly EventService eventService;
            private readonly IUserService userService;
            public Handler(EventService _eventService, IUserService _userService)
            {
                eventService = _eventService;
                userService = _userService;
            }

            public async Task<Unit> Handle(ParticipateInEventCommand request, CancellationToken cancellationToken)
            {
                var user = await userService.GetCurrentlySignedIn();
                await userService.ParticipateInEvent(request.EventToParticipate);
                await eventService.ParticipateInEvent(user.Id, request.EventToParticipate);

                return Unit.Value;
            }
        }
    }
}

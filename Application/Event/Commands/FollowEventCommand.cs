using Application.Message.Commands;
using MediatR;
using Persistence.Services.Database;

namespace Application.Event.Commands
{
    public class FollowEventCommand : IRequest<Unit>
    {
        public int EventToFollow { get; init; }

        public class Handler : IRequestHandler<FollowEventCommand, Unit>
        {
            private readonly EventService eventService;
            private readonly IUserService userService;
            public Handler(EventService _eventService, IUserService _userService)
            {
                eventService = _eventService;
                userService = _userService;
            }

            public async Task<Unit> Handle(FollowEventCommand request, CancellationToken cancellationToken)
            {
                await userService.FollowEvent(request.EventToFollow);

                return Unit.Value;
            }
        }
    }
}

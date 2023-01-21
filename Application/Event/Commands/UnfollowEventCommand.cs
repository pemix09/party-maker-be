using MediatR;
using Persistence.Services.Database;

namespace Application.Event.Commands
{
    public class UnfollowEventCommand : IRequest<Unit>
    {
        public int EventToUnfollow { get; init; }

        public class Handler : IRequestHandler<UnfollowEventCommand, Unit>
        {
            private readonly EventService eventService;
            private readonly IUserService userService;
            public Handler(EventService _eventService, IUserService _userService)
            {
                eventService = _eventService;
                userService = _userService;
            }

            public async Task<Unit> Handle(UnfollowEventCommand request, CancellationToken cancellationToken)
            {
                await userService.UnFollowEvent(request.EventToUnfollow);

                return Unit.Value;
            }
        }
    }
}

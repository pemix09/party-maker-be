using MediatR;

namespace Application.Event.Queries
{
    using Core.Models;
    using Persistence.Services.Database;

    public class GetAllOfCurrentUserQuery : IRequest<IEnumerable<Event>>
    {
        public class Handler : IRequestHandler<GetAllOfCurrentUserQuery, IEnumerable<Event>>
        {
            private readonly EventService eventService;
            private readonly IUserService userService;
            public Handler(EventService _eventService, IUserService _userService)
            {
                eventService = _eventService;
                userService = _userService;
            }

            public async Task<IEnumerable<Event>> Handle(GetAllOfCurrentUserQuery request, CancellationToken cancellationToken)
            {
                var currentUser = await userService.GetCurrentlySignedIn();

                return eventService.GetAllForCurrentUser(currentUser.Id);
            }
        }
    }
}

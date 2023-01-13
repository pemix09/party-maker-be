using MediatR;

namespace Application.Event.Queries
{
    using Core.Models;
    using Core.Responses;
    using Persistence.Services.Database;

    public class GetAllOfCurrentUserQuery : IRequest<GetAllEventsForCurrentUserResponse>
    {
        public class Handler : IRequestHandler<GetAllOfCurrentUserQuery, GetAllEventsForCurrentUserResponse>
        {
            private readonly EventService eventService;
            private readonly IUserService userService;
            public Handler(EventService _eventService, IUserService _userService)
            {
                eventService = _eventService;
                userService = _userService;
            }

            public async Task<GetAllEventsForCurrentUserResponse> Handle(GetAllOfCurrentUserQuery request, CancellationToken cancellationToken)
            {
                var currentUser = await userService.GetCurrentlySignedIn();
                var organized = eventService.GetOrganizedByCurrentUser(currentUser.Id);
                IEnumerable<Event> followed = new List<Event>();
                if(currentUser.Followed != null)
                {
                    followed = await eventService.GetFollowedEvents(currentUser.Followed);
                }

                return new GetAllEventsForCurrentUserResponse()
                {
                    Organized = organized.ToList(),
                    Followed = followed.ToList()
                };
            }
        }
    }
}

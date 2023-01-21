using MediatR;

namespace Application.Event.Queries
{
    using AutoMapper;
    using Core.Dto;
    using Core.Models;
    using Core.Responses;
    using Persistence.Services.Database;

    public class GetAllOfCurrentUserQuery : IRequest<GetAllEventsForCurrentUserResponse>
    {
        public class Handler : IRequestHandler<GetAllOfCurrentUserQuery, GetAllEventsForCurrentUserResponse>
        {
            private readonly EventService eventService;
            private readonly IUserService userService;
            private readonly IMapper mapper;
            public Handler(EventService _eventService, IUserService _userService, IMapper _mapper)
            {
                eventService = _eventService;
                userService = _userService;
                mapper = _mapper;
            }

            public async Task<GetAllEventsForCurrentUserResponse> Handle(GetAllOfCurrentUserQuery request, CancellationToken cancellationToken)
            {
                var currentUser = await userService.GetCurrentlySignedIn();
                IEnumerable<EventDto> organized = await eventService.GetOrganizedByCurrentUser(currentUser.Id);
                IEnumerable<EventDto> followed = new List<EventDto>();
                IEnumerable<EventDto> participates = new List<EventDto>();
                if(currentUser.Followed != null)
                {
                    followed = await eventService.GetEventsByList(currentUser.Followed);
                }
                if(currentUser.ParticipatesIn != null)
                {
                    participates = await eventService.GetEventsByList(currentUser.ParticipatesIn);
                }

                return new GetAllEventsForCurrentUserResponse()
                {
                    Organized = organized.ToList(),
                    Followed = followed.ToList(),
                    Participates = participates.ToList()
                };
            }
        }
    }
}

using AutoMapper;
using Core.Dto;
using Core.Models;
using Core.Responses;
using MediatR;
using Persistence.Services.Database;

namespace Application.Event.Queries
{
    public class GetAllMusicGenresQuery : IRequest<IEnumerable<MusicGenre>>
    {
        public class Handler : IRequestHandler<GetAllMusicGenresQuery, IEnumerable<MusicGenre>>
        {
            private readonly EventService eventService;

            public Handler(EventService _eventService)
            {
                eventService = _eventService;
            }

            public async Task<IEnumerable<MusicGenre>> Handle(GetAllMusicGenresQuery request, CancellationToken cancellationToken)
            {
                return await eventService.GetAllMusicGenres();
            }
        }
    }
}

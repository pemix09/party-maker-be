using Core.Models;
using MediatR;
using Persistence.Services.Database;

namespace Application.Event.Queries
{
    public class GetMusicGenreByIdQuery : IRequest<MusicGenre?>
    {
        public int Id { get; set; }
        public class Handler : IRequestHandler<GetMusicGenreByIdQuery, MusicGenre?>
        {
            private readonly EventService eventService;

            public Handler(EventService _eventService)
            {
                eventService = _eventService;
            }

            public async Task<MusicGenre?> Handle(GetMusicGenreByIdQuery request, CancellationToken cancellationToken)
            {
                var genres = await eventService.GetAllMusicGenres();
                return genres.FirstOrDefault(genre => genre.Id == request.Id);
            }
        }
    }
}

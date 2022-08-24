using Core.Models;
using Persistence.DbContext;

namespace Persistence.Repositories
{
    public class MusicGenreRepository : Repository<MusicGenre>, IMusicGenreRepository
    {
        public MusicGenreRepository(PartyMakerDbContext context) : base(context) { }

        public PartyMakerDbContext PartyMakerDbContext
        {
            get { return Context as PartyMakerDbContext; }
        }
    }
}

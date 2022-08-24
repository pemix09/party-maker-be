using Core.Models;
using Persistence.DbContext;

namespace Persistence.Repositories
{
    public class EntrancePassRepository : Repository<EntrancePass>, IEntrancePassRepository
    {
        public EntrancePassRepository(PartyMakerDbContext context) : base(context) { }

        public PartyMakerDbContext PartyMakerDbContext
        {
            get { return Context as PartyMakerDbContext; }
        }
    }
}

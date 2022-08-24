using Core.Models;
using Persistence.DbContext;

namespace Persistence.Repositories
{
    public class BanRepository : Repository<Ban>, IBanRepository
    {
        public BanRepository(PartyMakerDbContext context) : base(context) { }

        public PartyMakerDbContext PartyMakerDbContext
        {
            get { return Context as PartyMakerDbContext; }
        }
    }
}

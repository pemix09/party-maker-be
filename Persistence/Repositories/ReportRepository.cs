using Core.Models;
using Persistence.DbContext;

namespace Persistence.Repositories
{
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        public ReportRepository(PartyMakerDbContext context) : base(context) { }

        public PartyMakerDbContext PartyMakerDbContext
        {
            get { return Context as PartyMakerDbContext; }
        }
    }
}

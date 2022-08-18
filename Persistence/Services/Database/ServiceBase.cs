namespace Persistence.Services.Database
{
    using Persistence.DbContext;
    using Persistence.UnitOfWork;
    public class ServiceBase
    {
        protected readonly IUnitOfWork database;
        protected ServiceBase(PartyMakerDbContext context)
        {
            database = new UnitOfWork(context);
        }
    }
}

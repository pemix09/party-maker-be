namespace Persistence.Services.Database
{
    using Persistence.UnitOfWork;
    public class ServiceBase
    {
        protected readonly IUnitOfWork database;

        protected ServiceBase(IUnitOfWork _UnitOfWork)
        {
            database = _UnitOfWork;
        }
    }
}

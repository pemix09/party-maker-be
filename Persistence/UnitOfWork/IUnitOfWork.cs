using Persistence.Repositories;

namespace Persistence.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IEventRepository Events { get; }
    int Complete();

}
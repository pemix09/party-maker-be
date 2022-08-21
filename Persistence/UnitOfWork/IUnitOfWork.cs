using Persistence.Repositories;

namespace Persistence.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IEventRepository Events { get; }
    IMessageRepository Messages { get; }
    IBanRepository Bans { get; }
    IEntrancePassRepository EntrancePasses { get; }
    IMusicGenreRepository MusicGenres { get; }
    Task<int> Complete();

}
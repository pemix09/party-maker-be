using Persistence.Repositories;

namespace Persistence.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IEventRepository Events { get; }
    IMessageRepository Messages { get; }
    IBanRepository Bans { get; }
    IEntrancePassRepository EntrancePasses { get; }
    IMusicGenreRepository MusicGenres { get; }
    INotificationRepository Notifications { get; }
    IReportRepository Reports { get; }
    Task<int> Complete();

}
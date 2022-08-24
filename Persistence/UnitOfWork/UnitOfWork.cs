using Persistence.DbContext;
using Persistence.Repositories;

namespace Persistence.UnitOfWork;

//Unit of work contains repositories, we use it for every data operations.
public class UnitOfWork : IUnitOfWork
{
    private readonly PartyMakerDbContext context;
    public IEventRepository Events { get; private set; }
    public IMessageRepository Messages { get; private set; }
    public IBanRepository Bans { get; private set; }
    public IEntrancePassRepository EntrancePasses { get; private set; }
    public IMusicGenreRepository MusicGenres { get; private set; }
    public INotificationRepository Notifications { get; private set; }
    public IReportRepository Reports { get; private set; }

    public UnitOfWork(PartyMakerDbContext _context)
    {
        context = _context;
        
        Events = new EventRepository(context);
        Messages = new MessageRepository(context);
        Bans = new BanRepository(context);
        EntrancePasses = new EntrancePassRepository(context);
        MusicGenres = new MusicGenreRepository(context);
        Notifications = new NotificationRepository(context);
        Reports = new ReportRepository(context);
    }
    public async void Dispose()
    {
        await context.DisposeAsync();
    }

    public async Task<int> Complete()
    {
        return await context.SaveChangesAsync();
    }
}
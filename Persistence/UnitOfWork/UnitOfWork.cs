using Persistence.DbContext;
using Persistence.Repositories;

namespace Persistence.UnitOfWork;

//Unit of work contains repositories, we use it for every data operations.
public class UnitOfWork : IUnitOfWork
{
    private readonly PartyMakerDbContext Context;
    public IEventRepository Events { get; private set; }
    public IMessageRepository Messages { get; private set; }

    public UnitOfWork(PartyMakerDbContext context)
    {
        Context = context;
        
        //here we can add more repositories
        Events = new EventRepository(Context);
        Messages = new MessageRepository(Context);
    }
    public async void Dispose()
    {
        await Context.DisposeAsync();
    }

    public async Task<int> Complete()
    {
        return await Context.SaveChangesAsync();
    }
}
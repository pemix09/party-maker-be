using Core.Models;
using Persistence.DbContext;

namespace Persistence.Repositories;

public class MessageRepository : Repository<Message>, IMessageRepository
{
    public MessageRepository(PartyMakerDbContext context) : base(context) {}

    public PartyMakerDbContext PartyMakerDbContext
    {
        get {return Context as PartyMakerDbContext;}
    }
    public async Task<Message> Get(long _id)
    {
        return await PartyMakerDbContext.Messages.FindAsync(_id);
    }
}
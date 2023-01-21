using Core.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContext;
using System.Linq;

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

    public async Task<IEnumerable<Message>> GetAllForUser(string userId, int eventId)
    {
        var party = await PartyMakerDbContext.Events.FirstAsync(party => party.Id == eventId);
        var userMessages = await PartyMakerDbContext.Messages
                                    .Where(msg => msg.SenderId == userId)
                                    .Where(msg => msg.EventId == eventId)
                                    .ToListAsync();
        var organizerMessages = await PartyMakerDbContext.Messages
                                        .Where(msg => msg.EventId == eventId)
                                        .Where(msg => msg.SenderId == party.OrganizerId)
                                        .ToListAsync();

        userMessages.AddRange(organizerMessages);
        userMessages.OrderBy(msg => msg.Date);

        return userMessages;
    }

    public async Task<IEnumerable<Message>> GetAllForEvent(int eventId)
    {
        return await PartyMakerDbContext.Messages
                        .Where(msg => msg.EventId == eventId)
                        .OrderBy(msg => msg.Date)
                        .ToListAsync();

    }

    public async Task<Message> GetLastMessageForEvent(int eventId)
    {
        return await PartyMakerDbContext.Messages
                        .Where(msg => msg.EventId == eventId)
                        .OrderBy(msg => msg.Date)
                        .FirstOrDefaultAsync();
    }
}
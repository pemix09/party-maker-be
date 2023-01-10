using System.Data.Entity;
using Core.Models;
using Persistence.DbContext;

namespace Persistence.Repositories;

public class EventRepository : Repository<Event>, IEventRepository
{
    public EventRepository(PartyMakerDbContext context) : base(context) { }

    public PartyMakerDbContext PartyMakerDbContext
    {
        get {return Context as PartyMakerDbContext;}
    }
    public IEnumerable<Event> GetTopEvents(int count)
    {
        //we can include here some different logic
        return PartyMakerDbContext.Events.Take(count);
    }

    public IEnumerable<Event> GetForAreaByQuery(string query, double latNorth, double latSouth, double lonEast, double lonWest)
    {
        IEnumerable<Event> events = PartyMakerDbContext.Events;

        if(string.IsNullOrWhiteSpace(query) == false)
        {
            events = events.Where(x => x.Name.StartsWith(query));
        }
        events = events.Where(x => x.Longitude >= lonWest && x.Longitude <= lonEast);
        events = events.Where(x => x.Latitude >= latSouth && x.Latitude <= latNorth);

        return events.ToList();
    }

    public IEnumerable<Event> GetOrganizerEvents(string organizerId)
    {
        return PartyMakerDbContext.Events
            .Where(e => e.OrganizerId == organizerId)
            .ToList();
    }

    public Task RemoveAllForUser(string _userId)
    {
        IEnumerable<Event> eventsToDelete = PartyMakerDbContext.Events.Where(x => x.OrganizerId == _userId);
        
        PartyMakerDbContext.Events.RemoveRange(eventsToDelete);

        return Task.CompletedTask;
    }

}
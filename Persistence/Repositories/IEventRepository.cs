using Core.Models;

namespace Persistence.Repositories;

public interface IEventRepository : IRepository<Event>
{
    //here we can add additional methods for events like get top 10
    IEnumerable<Event> GetTopEvents(int count);
    IEnumerable<Event> GetOrganizerEvents(string organizerId);
    Task RemoveAllForUser(string _userId);
}
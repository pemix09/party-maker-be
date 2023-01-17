﻿namespace Persistence.Services.Database
{
    using Persistence.UnitOfWork;
    using Core.Models;
    using Persistence.DbContext;

    public class EventService : ServiceBase
    {
        public EventService(PartyMakerDbContext _context) : base(_context) { }
        public async Task AddToDataBase(Event _event)
        {
            await database.Events.Add(_event);
            await database.Complete();
        }

        public async Task UpdateInDataBase(Event _edited)
        {
            database.Events.Update(_edited);
            await database.Complete();
        }
        public async Task DeleteFromDataBase(int _id)
        {
            Event toDelete = await database.Events.Get(_id);
            database.Events.Remove(toDelete);
            await database.Complete();
        }

        public async Task<Event> GetByIdFromDataBase(int _id)
        {
            return await database.Events.Get(_id);
        }
        public async Task<IEnumerable<Event>> GetAllFromDataBase()
        {
            return await database.Events.GetAll();
        }

        public IEnumerable<Event> GetForAreaByQuery(string query, double latNorth, double latSouth, double lonEast, double lonWest)
        {
            return database.Events.GetForAreaByQuery(query, latNorth, latSouth, lonEast, lonWest);
        }

        public IEnumerable<Event> GetForArea(double latNorth, double latSouth, double lonEast, double lonWest)
        {
            return database.Events.GetForArea(latNorth, latSouth, lonEast, lonWest);
        }

        public IEnumerable<Event> GetOrganizedByCurrentUser(string userId)
        {
            return database.Events.GetOrganizerEvents(userId);
        }

        public async Task<IEnumerable<Event>> GetFollowedEvents(List<int> eventsId)
        {
            List<Event> followed = new List<Event>();
            
            foreach(var id in eventsId)
            {
                var searchedEvent = await database.Events.Get(id);
                if(searchedEvent != null)
                {
                    followed.Add(searchedEvent);
                }
            }
            return followed;
        }
    }
}

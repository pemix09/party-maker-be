namespace Persistence.Services.Database
{
    using Persistence.Exceptions;
    using Core.Models;
    using Persistence.DbContext;
    using Core.Dto;
    using AutoMapper;

    public class EventService : ServiceBase
    {
        private readonly IMapper mapper;
        public EventService(PartyMakerDbContext _context, IMapper _mapper) : base(_context) 
        { 
            mapper= _mapper;
        }
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

        public async Task<IEnumerable<MusicGenre>> GetAllMusicGenres()
        {
            return await database.MusicGenres.GetAll();
        }

        public IEnumerable<Event> GetForAreaByQuery(string query, double latNorth, double latSouth, double lonEast, double lonWest)
        {
            return database.Events.GetForAreaByQuery(query, latNorth, latSouth, lonEast, lonWest);
        }

        public IEnumerable<Event> GetByQuery(string query)
        {
            return database.Events.GetByQuery(query);
        }
        public IEnumerable<Event> GetForArea(double latNorth, double latSouth, double lonEast, double lonWest)
        {
            return database.Events.GetForArea(latNorth, latSouth, lonEast, lonWest);
        }

        public async Task ParticipateInEvent(string userId, int eventId)
        {
            var party = await database.Events.Get(eventId);
            if(party == null)
            {
                throw new EventNotFoundException();
            }

            if(party.ParticipatorsIds == null)
            {
                party.ParticipatorsIds = new();
            }

            party.ParticipatorsIds.Add(userId);
            await database.Complete();
        }
        public async Task NotParticipateInEvent(string userId, int eventId)
        {
            var party = await database.Events.Get(eventId);
            if (party == null)
            {
                throw new EventNotFoundException();
            }

            if (party.ParticipatorsIds != null && party.ParticipatorsIds.Contains(userId))
            {
                party.ParticipatorsIds.Remove(userId);
            }
            await database.Complete();
        }
        public async Task<IEnumerable<EventDto>> GetOrganizedByCurrentUser(string userId)
        {
            var events =  database.Events.GetOrganizerEvents(userId);
            var organizes = new List<EventDto>();

            foreach(var party in events)
            {
                var eventDto = mapper.Map<EventDto>(party);
                var lastMessage = await database.Messages.GetLastMessageForEvent(party.Id);
                if(lastMessage != null)
                {
                    eventDto.LastMessage = lastMessage.Content;
                    eventDto.LastMessageTime = lastMessage.Date;
                }

                organizes.Add(eventDto);
            }

            return organizes;
        }

        public async Task<IEnumerable<EventDto>> GetEventsByList(List<int> eventsId)
        {
            List<EventDto> followed = new List<EventDto>();
            
            foreach(var id in eventsId)
            {
                var searchedEvent = mapper.Map<EventDto>(await database.Events.Get(id));
                
                if(searchedEvent != null)
                {
                    var lastMessage = await database.Messages.GetLastMessageForEvent(searchedEvent.Id);
                    if (lastMessage != null)
                    {
                        searchedEvent.LastMessage = lastMessage.Content;
                        searchedEvent.LastMessageTime = lastMessage.Date;
                    }
                    followed.Add(searchedEvent);
                }
            }
            return followed;
        }
    }
}

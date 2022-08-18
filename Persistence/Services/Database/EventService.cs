namespace Persistence.Services.Database
{
    using Persistence.UnitOfWork;
    using Core.Models;
    public class EventService : ServiceBase
    {
        public EventService(IUnitOfWork _UnitOfWork) : base(_UnitOfWork) { }
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
        public async Task DeleteInDataBase(int _id)
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
    }
}

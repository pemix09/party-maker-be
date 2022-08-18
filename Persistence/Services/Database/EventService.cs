namespace Persistence.Services.Database
{
    using Persistence.UnitOfWork;
    using Core.Models;
    public class EventService : ServiceBase
    {
        public EventService(IUnitOfWork _UnitOfWork) : base(_UnitOfWork) { }
        public async Task Create(Event _event)
        {
            await database.Events.Add(_event);
            await database.Complete();
        }

        public async Task Update(Event _edited)
        {
            database.Events.Update(_edited);
            await database.Complete();
        }
        public async Task Delete(int _id)
        {
            Event toDelete = await database.Events.Get(_id);
            database.Events.Remove(toDelete);
            await database.Complete();
        }

        public async Task<Event> GetById(int _id)
        {
            return await database.Events.Get(_id);
        }
        public async Task<IEnumerable<Event>> GetAll()
        {
            return await database.Events.GetAll();
        }
    }
}

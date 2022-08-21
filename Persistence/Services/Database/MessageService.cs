namespace Persistence.Services.Database
{
    using Persistence.DbContext;
    using Core.Models;
    public class MessageService : ServiceBase
    {
        public MessageService(PartyMakerDbContext _context) : base(_context) { }

        public async Task AddToDataBase(Message _message)
        {
            await database.Messages.Add(_message);
            await database.Complete();
        }

        public async Task UpdateInDataBase(Message _edited)
        {
            database.Messages.Update(_edited);
            await database.Complete();
        }
        public async Task DeleteFromDataBase(long _id)
        {
            Message toDelete = await database.Messages.Get(_id);
            database.Messages.Remove(toDelete);
            await database.Complete();
        }

        public async Task<Message> GetByIdFromDataBase(long _id)
        {
            return await database.Messages.Get(_id);
        }
        public async Task<IEnumerable<Message>> GetAllFromDataBase()
        {
            return await database.Messages.GetAll();
        }
    }
}

using Core.Models;
using Persistence.DbContext;

namespace Persistence.Services.Database
{
    public class EntrancePassService : ServiceBase
    {
        public EntrancePassService(PartyMakerDbContext context) : base(context){}
        public async Task AddToDataBase(EntrancePass _pass)
        {
            await database.EntrancePasses.Add(_pass);
            await database.Complete();
        }

        public async Task UpdateInDataBase(EntrancePass _edited)
        {
            database.EntrancePasses.Update(_edited);
            await database.Complete();
        }
        public async Task DeleteFromDataBase(int _id)
        {
            EntrancePass toDelete = await database.EntrancePasses.Get(_id);
            database.EntrancePasses.Remove(toDelete);
            await database.Complete();
        }

        public async Task<EntrancePass> GetByIdFromDataBase(int _id)
        {
            return await database.EntrancePasses.Get(_id);
        }
        public async Task<IEnumerable<EntrancePass>> GetAllFromDataBase()
        {
            return await database.EntrancePasses.GetAll();
        }
    }
}

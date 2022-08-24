using Core.Models;
using Persistence.DbContext;

namespace Persistence.Services.Database
{
    public class BanService : ServiceBase
    {
        public BanService(PartyMakerDbContext _context) : base(_context) { }
        public async Task AddToDataBase(Ban _ban)
        {
            await database.Bans.Add(_ban);
            await database.Complete();
        }

        public async Task UpdateInDataBase(Ban _ban)
        {
            database.Bans.Update(_ban);
            await database.Complete();
        }
        public async Task DeleteFromDataBase(int _id)
        {
            Ban toDelete = await database.Bans.Get(_id);
            database.Bans.Remove(toDelete);
            await database.Complete();
        }

        public async Task<Ban> GetByIdFromDataBase(int _id)
        {
            return await database.Bans.Get(_id);
        }
        public async Task<IEnumerable<Ban>> GetAllFromDataBase()
        {
            return await database.Bans.GetAll();
        }
    }
}

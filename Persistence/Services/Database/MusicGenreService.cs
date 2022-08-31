using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Services.Database
{
    using Persistence.DbContext;
    public class MusicGenreService : ServiceBase
    {
        public MusicGenreService(PartyMakerDbContext _context) : base(_context){}
    }
}
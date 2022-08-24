using Core.Models;
using Persistence.DbContext;

namespace Persistence.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(PartyMakerDbContext context) : base(context) { }

        public PartyMakerDbContext PartyMakerDbContext
        {
            get { return Context as PartyMakerDbContext; }
        }
    }
}

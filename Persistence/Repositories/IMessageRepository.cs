using Core.Models;

namespace Persistence.Repositories;

public interface IMessageRepository : IRepository<Message>
{

    //here we can add some custom methods, if those in IRepository are not sufficient
    Task<Message> Get(long id);

}
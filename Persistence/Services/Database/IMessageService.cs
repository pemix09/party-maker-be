using Core.Models;

namespace Persistence.Services.Database;

public interface IMessageService
{
    Task AddToDataBase(Message _message);
    Task UpdateInDataBase(Message _edited);
    Task DeleteFromDataBase(long _id);
    Task<Message> GetByIdFromDataBase(long _id);
    Task<IEnumerable<Message>> GetAllFromDataBase();
    Task<IEnumerable<Message>> GetAllForUser(string userId, int eventId);
    Task<IEnumerable<Message>> GetAllForEvent(int eventId);
}
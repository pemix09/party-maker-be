namespace Infrastructure.Hubs;

public interface IMessageHub
{
    Task NewMessage(string fromUser, string content);

}
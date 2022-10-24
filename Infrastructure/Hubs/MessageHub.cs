using Core.Models;

namespace Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

public class MessageHub : Hub<IMessageHub>
{
    public MessageHub(){}

    public async Task NewMessage(string fromUser, string content)
    {
        var msg = Message.Create(fromUser, fromUser, 0, content);
        Streaming(msg);
    }

    public async IAsyncEnumerable<Message> Streaming(Message _msg)
    {
        yield return _msg;
    }

}
using Core.Models;

namespace Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

public class MessageHub : Hub
{
    public async IAsyncEnumerable<Message> MessageStreaming(CancellationToken _cancellationToken)
    {
        while (true)
        {
            yield return Message.Create("dsa", "dsa", 12, "cs");
            await Task.Delay(1000, _cancellationToken);
        }
    }
}
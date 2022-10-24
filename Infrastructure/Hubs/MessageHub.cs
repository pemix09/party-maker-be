namespace Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;

public class MessageHub : Hub<IMessageHub>
{
    public MessageHub(){}

}
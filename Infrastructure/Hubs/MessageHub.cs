using Core.Models;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Hubs;

public class MessageHub : Hub<IMessageHub>
{
    public MessageHub(){}
    

}
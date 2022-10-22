using Core.Models;

namespace Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using MediatR;

public class MessageHub : Hub<IMessageHub>
{
    public MessageHub(){}

}
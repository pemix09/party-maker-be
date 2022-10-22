using Infrastructure.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.NotificationsHandlers;

public class MessageSentNotificationHandler : INotificationHandler<MessageSentNotification>
{
    private readonly IHubContext<MessageHub, IMessageHub> messageHubContext;

    public MessageSentNotificationHandler(
        IHubContext<MessageHub, IMessageHub> _messageHubContext)
    {
        messageHubContext = _messageHubContext;
    }

    public async Task Handle(
        MessageSentNotification notification,
        CancellationToken cancellationToken)
    {
        // read relevant data from notification
        string recipientUserId = notification.ToUserId;
        string fromUsername = notification.FromUserName;
        string subject = notification.MessageSubject;

        // We use the context to avoid magic strings
        // Our Id is also our authentication
        //   identifier, which is used by SignalR by default too
        await messageHubContext
            .Clients
            .User(recipientUserId)
            .NewMessage(fromUsername, subject);
    }
}
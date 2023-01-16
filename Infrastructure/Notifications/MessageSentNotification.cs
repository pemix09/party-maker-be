using Core.Models;

namespace Infrastructure;
using MediatR;

public class MessageSentNotification : INotification
{
    public string ToUserId { get; }
    public string FromUserId { get; }
    public string MessageContent { get; }
    public DateTimeOffset MessageTime { get; init; }

    private MessageSentNotification(
        string fromUserId,
        string _messageContent,
        DateTimeOffset _messageTime)
    {
        FromUserId = fromUserId;
        MessageContent = _messageContent;
        MessageTime = _messageTime; 
    }

    public static MessageSentNotification CreateFromMessage(Message _message)
    {
        return new MessageSentNotification(_message.SenderId, _message.Content, _message.Date);
    }
}
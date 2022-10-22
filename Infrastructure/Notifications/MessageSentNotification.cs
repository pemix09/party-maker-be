namespace Infrastructure;
using MediatR;

public class MessageSentNotification : INotification
{
    public string ToUserId { get; }
    public string FromUserId { get; }
    public string FromUserName { get; }
    public int MessageId { get; }
    public string MessageSubject { get; }

    public MessageSentNotification(
        string toUserId,
        string fromUserId, string fromUserName,
        int messageId, string messageSubject)
    {
        ToUserId = toUserId;
        FromUserId = fromUserId;
        FromUserName = fromUserName;
        MessageId = messageId;
        MessageSubject = messageSubject;
    }
}
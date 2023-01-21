using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Message
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }
    
    [Required]
    public string SenderId { get; set; }
    
    [Required]
    public string SenderName { get; set; }
    
    [Required]
    public DateTimeOffset Date { get; set; }
    
    [Required]
    public int EventId { get; set; }
    
    [Required]
    public string Content { get; set; }
    public bool Read { get; set; }
    public Message() { }
    private Message(string _senderId,string _senderName, int _eventID, string _content)
    {
        SenderName = _senderName;
        SenderId = _senderId;
        EventId = _eventID;
        Content = _content;
        Date = DateTimeOffset.Now;
    }
    public static Message Create(string _senderId, string _senderName, int _eventID, string _content)
    {
        return new Message(_senderId, _senderName, _eventID, _content); 
    }

    public void SetRead(bool _read = true)
    {
        Read = _read;
    }
    public void SetContent(string _NewContent)
    {
        Content = _NewContent;
    }
}
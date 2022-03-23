using System;

namespace Core.Models;

public class Message
{
    public AppUser Sender { get; set; }
    public AppUser Receiver { get; set; }
    public DateTime Date { get; set; }
    public Event Event { get; set; }
    public string Content { get; set; }
    public bool Read { get; set; }
}
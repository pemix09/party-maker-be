namespace Core.Models;

public class Notification
{
    public int Id { get; set; }
    public Event Event { get; set; }
    public AppUser Receiver { get; set; }
    public string Content { get; set; }
    public bool Read { get; set; }
}
namespace Core.Models;

public class ChatGroup
{
    public int Id { get; set; }
    public List<AppUser> Users { get; set; }
    public List<Message> Messages { get; set; }
}
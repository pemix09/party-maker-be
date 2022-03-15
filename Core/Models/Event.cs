namespace Core.Models;

public class Event
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Place { get; set; }
    public AppUser Organizer { get; set; }
    public string Pass { get; set; }
    public string Photo { get; set; }
    public MusicGenre MusicGenre { get; set; }
    public ChatGroup ChatGroup { get; set; }
}
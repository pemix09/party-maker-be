using System;

namespace Core.Models;
using System.Collections.Generic;

public class Event
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Place { get; set; }
    public AppUser Organizator { get; set; }
    public List<AppUser> Participators { get; set; }
    public EntrancePass Pass { get; set; }
    public string Photo { get; set; }
    public MusicGenre MusicGenre { get; set; }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;
using System.Collections.Generic;

public class Event
{
    [Required]
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public string Place { get; set; }
    
    [Required]
    public int OrganizatorId { get; set; }
    public List<int> ParticipatorsIds { get; set; }
    
    [Required]
    public EntrancePass Pass { get; set; }
    public string Photo { get; set; }
    public MusicGenre MusicGenre { get; set; }

    public Event(){}
    private Event(
        string description,
        DateTime date,
        string place,
        int organizatorId,
        EntrancePass pass,
        string photo,
        MusicGenre musicGenre)
    {
        this.Description = description;
        this.Date = date;
        this.Place = place;
        this.OrganizatorId = organizatorId;
        this.Pass = pass;
        this.Photo = photo;
        this.MusicGenre = musicGenre;
    }

    public static Event Create(
        string description,
        DateTime date,
        string place,
        int organizatorId,
        EntrancePass pass,
        string photo,
        MusicGenre musicGenre
    )
    {
        return new Event(description, date, place, organizatorId, pass, photo, musicGenre);
    }
}
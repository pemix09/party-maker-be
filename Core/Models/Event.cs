using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

public class Event
{
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public string Place { get; set; }
    
    [Required]
    public AppUser Organizer { get; set; }
    
    public List<AppUser> Participants { get; set; } = new List<AppUser>();
    
    [Required]
    public int PassId { get; set; }
    public string Photo { get; set; }
    public MusicGenre MusicGenre { get; set; }
    public string Type { get; set; }

    public Event(){}
    private Event(
        string description,
        string place,
        AppUser organizer,
        int pass,
        string photo,
        MusicGenre musicGenreId,
        string type)
    {
        this.Description = description;
        this.Date = DateTime.Now;
        this.Place = place;
        this.Organizer = organizer;
        this.PassId = pass;
        this.Photo = photo;
        this.MusicGenre = musicGenreId;
        Type = type;
    }

    public static Event Create(
        string description,
        string place,
        AppUser organizer,
        int EntrancePassId,
        string photo,
        MusicGenre musicGenre,
        string type
    )
    {
        return new Event(description, place, organizer, EntrancePassId, photo, musicGenre, type);
    }
}
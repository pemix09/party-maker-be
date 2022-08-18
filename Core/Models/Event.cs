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
    
    public List<int>? ParticipatorsIds { get; set; }
    
    [Required]
    public int PassId { get; set; }
    public string Photo { get; set; }
    public int MusicGenreId { get; set; }

    public Event(){}
    private Event(
        string description,
        string place,
        int organizatorId,
        int pass,
        string photo,
        int musicGenreId)
    {
        this.Description = description;
        this.Date = DateTime.Now;
        this.Place = place;
        this.OrganizatorId = organizatorId;
        this.PassId = pass;
        this.Photo = photo;
        this.MusicGenreId = musicGenreId;
    }

    public static Event Create(
        string description,
        string place,
        int organizatorId,
        int EntrancePassId,
        string photo,
        int musicGenreId
    )
    {
        return new Event(description, place, organizatorId, EntrancePassId, photo, musicGenreId);
    }
}
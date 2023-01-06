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
    public DateTimeOffset Date { get; set; }
    
    [Required]
    public string Place { get; set; }
    
    [Required]
    public string OrganizerId { get; set; }
    
    public List<int>? ParticipatorsIds { get; set; }

    [Required]
    public double Latitude { get; set; }

    [Required]
    public double Longitude { get; set; }

    [Required]
    public int PassId { get; set; }
    public string Photo { get; set; }
    public int MusicGenreId { get; set; }
    public string Type { get; set; }

    public Event(){}
    private Event(
        string description,
        string place,
        string organizerId,
        int pass,
        string photo,
        int musicGenreId,
        string type,
        double latitude,
        double longitude)
    {
        this.Description = description;
        this.Date = DateTime.Now;
        this.Place = place;
        this.OrganizerId = organizerId;
        this.PassId = pass;
        this.Photo = photo;
        this.MusicGenreId = musicGenreId;
        this.Latitude = latitude;
        this.Longitude = longitude;
        Type = type;
    }

    public static Event Create(
        string description,
        string place,
        string organizerId,
        int EntrancePassId,
        string photo,
        int musicGenreId,
        string type,
        double latitude,
        double longitude
    )
    {
        return new Event(description, place, organizerId, EntrancePassId, photo, musicGenreId, type, latitude, longitude) ;
    }
}
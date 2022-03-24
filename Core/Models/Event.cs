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
}
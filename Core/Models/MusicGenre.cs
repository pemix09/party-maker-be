using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class MusicGenre
{
    [Required]
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
}
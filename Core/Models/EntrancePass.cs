using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class EntrancePass
{
    [Required]
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string PassType { get; set; }
    public float? Price { get; set; }
}
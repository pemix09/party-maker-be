using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Notification
{
    [Required]
    [Key]
    public int Id { get; set; }
    
    public Event Event { get; set; }
    
    [Required]
    public int ReceiverId { get; set; }
    
    [Required]
    public string Content { get; set; }
    public bool Read { get; set; }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Message
{
    [Key]
    [Required]
    public long Id { get; set; }
    
    [Required]
    public int SenderId { get; set; }
    
    [Required]
    public int ReceiverId { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public Event Event { get; set; }
    
    [Required]
    public string Content { get; set; }
    public bool Read { get; set; }
}
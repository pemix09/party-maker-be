using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Notification
{
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public Event Event { get; set; }
    
    [Required]
    public string ReceiverId { get; set; }
    
    [Required]
    public string Content { get; set; }
    public bool Read { get; private set; }

    public void SetRead() => this.Read = true;
    public Notification(){}
    private Notification(Event _event, string _receiverId, string _content)
    {
        this.Event = _event;
        this.ReceiverId = _receiverId;
        this.Content = _content;
    }

    public static Notification Create(Event _event, string _receiverId, string _content)
    {
        return new Notification(_event, _receiverId, _content);
    }
}
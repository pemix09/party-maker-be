namespace Core.Models;

public class Ban
{
    public int Id { get; set; }
    public string Reason { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}
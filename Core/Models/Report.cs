namespace Core.Models;

public class Report
{
    public AppUser Reporter { get; set; }
    public AppUser Reported { get; set; }
    public DateTime Date { get; set; }
    public string Reason { get; set; }
}
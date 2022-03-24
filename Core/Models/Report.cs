using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models;

public class Report
{
    [Required]
    [Key]
    public int Id { get; private set; }
    
    [Required]
    public int ReporterId { get; private set; }
    
    [Required]
    public int ReportedId { get; private set; }
    
    [Required]
    public DateTime Date { get; private set; }
    
    [Required]
    public string Reason { get; private set; }

    public void SetReason(string newReason) => this.Reason = newReason;
    public void SetReportedId(int newReportedId) => this.ReportedId = newReportedId;
    public void SetReporterId(int newReporterId) => this.ReporterId = newReporterId;
    private Report(){}
    private Report(int reporterId, int reportedId, string reason)
    {
        this.ReporterId = reporterId;
        this.ReportedId = reportedId;
        this.Reason = reason;
        this.Date = DateTime.Now;
    }

    public static Report Create(int reporterId, int reportedId, string reason)
    {
        return new Report(reporterId, reportedId, reason);
    }
}
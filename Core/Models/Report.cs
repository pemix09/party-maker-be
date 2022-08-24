using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Report
{
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    
    [Required]
    public string ReporterId { get; private set; }
    
    [Required]
    public string ReportedId { get; private set; }
    
    [Required]
    public DateTime Date { get; private set; }
    
    [Required]
    public string Reason { get; private set; }

    public void SetReason(string _newReason) => this.Reason = _newReason;
    public void SetReportedId(string _newReportedId) => this.ReportedId = _newReportedId;
    public void SetReporterId(string _newReporterId) => this.ReporterId = _newReporterId;
    private Report(){}
    private Report(string _reporterId, string _reportedId, string _reason)
    {
        this.ReporterId = _reporterId;
        this.ReportedId = _reportedId;
        this.Reason = _reason;
        this.Date = DateTime.Now;
    }

    public static Report Create(string reporterId, string reportedId, string reason)
    {
        return new Report(reporterId, reportedId, reason);
    }
}
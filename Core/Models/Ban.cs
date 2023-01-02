using System;

namespace Core.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Ban
{
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Reason { get; set; }
    public DateTimeOffset Start { get; set; }
    public DateTimeOffset End { get; set; }
    public string BannedUserId { get; set; }
    public string ResponsibleAdmin { get; set; }
    public void SetNewEnd(DateTimeOffset _newEnd)
    {
        End = _newEnd;
    }
    public Ban() { }
    private Ban(string _reason, DateTimeOffset _end, string _bannedUserId, string _responsibleAdmin)
    {
        Reason = _reason;
        Start = DateTimeOffset.Now;
        End = _end;
        BannedUserId = _bannedUserId;
        ResponsibleAdmin = _responsibleAdmin;
    }
    public static Ban Create(string _reason, DateTimeOffset _end, string _bannedUserId, string _responsibleAdmin)
    {
        return new Ban(_reason, _end, _bannedUserId, _responsibleAdmin);
    }
}
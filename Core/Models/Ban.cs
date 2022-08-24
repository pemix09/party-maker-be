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
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public Ban() { }
    private Ban(string _reason, DateTime _end)
    {
        Reason = _reason;
        Start = DateTime.Now;
        End = _end;
    }
    public static Ban Create(string _reason, DateTime _end)
    {
        return new Ban(_reason, _end);
    }
}
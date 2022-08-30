using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Core.Models;

public class AppUser : IdentityUser
{
    public string? Photo { get; set; }
    public string? Avatar { get; set; }
    public float Rating { get; set; }
    public bool Premium { get; set; }
    public Ban? Ban { get; set; }
    public List<Event>? Followed { get; set; } = new List<Event>();
    public List<Event> OrganizedEvents { get; set; } = new List<Event>();
    public List<Event> TakesPart { get; set; } = new List<Event>();

    public void AddEvent(Event _event)
    {
        OrganizedEvents.Add(_event);
    }
    private AppUser(string _email, string _userName)
    {
        base.Email = _email;
        base.UserName = _userName;
        Rating = 0;
        Premium = false;
    }
    private AppUser() { }
    public static AppUser Create(string _email, string _userName)
    {
        return new AppUser(_email, _userName);
    }
}
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Core.Models;

public class AppUser : IdentityUser
{
    public string Photo { get; set; }
    public string Avatar { get; set; }
    public float Rating { get; set; }
    public bool Premium { get; set; }
    public int? BanId { get; set; }
    public List<Event> Followed { get; set; }

    private AppUser(string _email)
    {
        base.Email = _email;
    }
    private AppUser() { }
    public static AppUser Create(string _email)
    {
        return new AppUser(_email);
    }
}
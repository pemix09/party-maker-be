using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Core.Models;

public class AppUser : IdentityUser
{
    public string? Photo { get; set; }
    public string? Avatar { get; set; }
    public float Rating { get; set; }
    public bool Premium { get; set; }
    public int? BanId { get; set; }
    public List<Event>? Followed { get; set; }

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
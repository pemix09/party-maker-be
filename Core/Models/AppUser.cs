using System.Collections.Generic;
using Core.UtilityClasses;
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
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpires { get; set; }
    public DateTime RefreshTokenCreated { get; set; }
    public string AccessToken { get; set; }

    public void SetRefreshToken(RefreshToken _refreshToken)
    {
        RefreshToken = _refreshToken.Token;
        RefreshTokenCreated = _refreshToken.Created;
        RefreshTokenExpires = _refreshToken.Expires;
    }

    public void SetAccessToken(string _accessToken)
    {
        AccessToken = _accessToken;
    }
    private AppUser(string _email, string _userName)
    {
        base.Email = _email;
        base.UserName = _userName;
        Rating = 0;
        Premium = false;
    }
    public AppUser() { }
    public static AppUser Create(string _email, string _userName)
    {
        return new AppUser(_email, _userName);
    }
}
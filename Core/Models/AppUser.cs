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
    public string? RefreshToken { get; set; }
    public DateTimeOffset RefreshTokenExpires { get; set; }
    public DateTimeOffset RefreshTokenCreated { get; set; }
    public string? AccessToken { get; set; }
    public DateTimeOffset AccessTokenExpires { get; set; }
    public DateTimeOffset AccessTokenCreated { get; set; }

    public void SetRefreshToken(RefreshToken _refreshToken)
    {
        RefreshToken = _refreshToken.Token;
        RefreshTokenCreated = _refreshToken.Created;
        RefreshTokenExpires = _refreshToken.Expires;
    }

    public void SetAccessToken(AccessToken _accessToken)
    {
        AccessToken = _accessToken.Token;
        AccessTokenExpires = _accessToken.Expires;
        AccessTokenCreated = _accessToken.Created;
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
using System.Collections.Generic;
using Core.UtilityClasses;
using Microsoft.AspNetCore.Identity;

namespace Core.Models;

public class AppUser : IdentityUser
{
    public string? Photo { get; set; }
    public float? AvarageRating { get; set; }
    public int? AmmountOfRatings { get; set; }
    public float[]? Ratings { get; set; }
    public bool? Premium { get; set; }
    public int? BanId { get; set; }
    public List<int>? Followed { get; set; }
    public List<int>? ParticipatesIn { get; set; }

    //TODO
    //public List<int>? Organizes { get; set; }
    public string? RefreshToken { get; set; }
    public DateTimeOffset RegistrationDate { get; set; }
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
        AvarageRating = 5;
        Premium = false;
        RegistrationDate = DateTimeOffset.Now;
    }
    public AppUser() { }
    public static AppUser Create(string _email, string _userName)
    {
        return new AppUser(_email, _userName);
    }
}
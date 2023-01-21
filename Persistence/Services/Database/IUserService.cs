using Core.Dto;
using Core.Models;
using Core.UtilityClasses;

namespace Persistence.Services.Database;

public interface IUserService
{
    Task Register(AppUser _newUser, string _password);
    Task<LoginResponse> Login(string _email, string _password);
    Task Logout();
    Task DeleteCurrent();
    Task<AppUser> GetCurrentlySignedIn();
    Task<AppUser> GetUserById(string _Id);
    Task UnFollowEvent(int eventId);
    Task FollowEvent(int eventId);
    Task ChangePassword(string _userId, string _oldPassword, string _newPassword);
    Task UpdateUser(AppUserDto user);
}
using Core.Models;

namespace Persistence.Services.Database;

public interface IUserService
{
    Task Register(AppUser _newUser, string _password);
    Task<string> Login(string _email, string _password);
    Task Logout();
    Task DeleteCurrent();
    Task<AppUser> GetCurrentlySignedIn();
    Task<AppUser> GetUserById(string _Id);
}
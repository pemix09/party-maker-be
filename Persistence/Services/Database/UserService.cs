using Core.Models;
using Microsoft.AspNetCore.Identity;
using Persistence.DbContext;

namespace Persistence.Services.Database
{
    public class UserService : ServiceBase
    {
        private UserManager<AppUser> userManager { get; init; }
        public UserService(
            PartyMakerDbContext _context, 
            UserManager<AppUser> _userManager) : base(_context) 
        {
            userManager = _userManager;
        }

        public async Task Register(AppUser _newUser, string _password)
        {
            AppUser user = await userManager.CreateAsync(_newUser);
            await userManager.AddPasswordAsync(user, _password);
            await userManager.AddToRoleAsync(user, "User");
        }
    }
}

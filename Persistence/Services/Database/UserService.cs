using Core.Models;
using Microsoft.AspNetCore.Identity;
using Persistence.DbContext;

namespace Persistence.Services.Database
{
    public class UserService : ServiceBase
    {
        private UserManager<AppUser> userManager { get; init; }
        private SignInManager<AppUser> signInManager { get; init; }
        private bool stayLoggedAfterClosingBrowser = true;
        private bool lockAccountAfterSignInFailure = false;
        public UserService(
            PartyMakerDbContext _context, 
            UserManager<AppUser> _userManager,
            SignInManager<AppUser> _signInManager) : base(_context) 
        {
            userManager = _userManager;
        }

        public async Task Register(AppUser _newUser, string _password)
        {
            AppUser user = await userManager.CreateAsync(_newUser);
            await userManager.AddPasswordAsync(user, _password);
            await userManager.AddToRoleAsync(user, "User");
        }

        public async Task Login(string _email, string _password)
        {
            AppUser user = await  userManager.FindByEmailAsync(_email);
            await signInManager.PasswordSignInAsync(_email, _password, stayLoggedAfterClosingBrowser, lockAccountAfterSignInFailure);
        }
    }
}

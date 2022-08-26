using Core.Models;
using Microsoft.AspNetCore.Identity;
using Persistence.DbContext;
using Persistence.Exceptions;

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
            await userManager.CreateAsync(_newUser);
            await userManager.AddPasswordAsync(_newUser, _password);
            await userManager.AddToRoleAsync(_newUser, "User");
        }

        public async Task Login(string _email, string _password)
        {
            AppUser user = await  userManager.FindByEmailAsync(_email);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            await signInManager.PasswordSignInAsync(_email, _password, stayLoggedAfterClosingBrowser, lockAccountAfterSignInFailure);
        }
    }
}

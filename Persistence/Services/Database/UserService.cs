using Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Persistence.DbContext;
using Persistence.Exceptions;
using System.Security.Claims;

namespace Persistence.Services.Database
{
    public class UserService : ServiceBase
    {
        private UserManager<AppUser> userManager { get; init; }
        private SignInManager<AppUser> signInManager { get; init; }
        private IHttpContextAccessor httpContextAccesor { get; init; }
        private bool stayLoggedAfterClosingBrowser = true;
        private bool lockAccountAfterSignInFailure = false;
        public UserService(
            PartyMakerDbContext _context, 
            UserManager<AppUser> _userManager,
            SignInManager<AppUser> _signInManager,
            IHttpContextAccessor _httpContextAccesor) : base(_context) 
        {
            userManager = _userManager;
            signInManager = _signInManager;
            httpContextAccesor = _httpContextAccesor;
        }

        public async Task Register(AppUser _newUser, string _password)
        {
            var result = await userManager.CreateAsync(_newUser);

            if(!result.Succeeded)
            {
                throw new UserCannotBeCreatedException(result.Errors);
            }

            await userManager.AddPasswordAsync(_newUser, _password);
            await userManager.AddToRoleAsync(_newUser, "User");
        }

        public async Task Login(string _email, string _password)
        {
            AppUser user = await userManager.FindByEmailAsync(_email);

            if (user == null)
            {
                throw new UserNotFoundException(_email);
            }

            var result = await signInManager.PasswordSignInAsync(user.UserName, _password, stayLoggedAfterClosingBrowser, lockAccountAfterSignInFailure);

            if(!result.Succeeded)
            {
                throw new UserCannotBeSignInException(result, user);
            }

            var claims = await userManager.GetClaimsAsync(user);

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await httpContextAccesor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }

        public async Task Logout()
        {
            if(IsUserSignedIn())
            {
                await signInManager.SignOutAsync();
                await httpContextAccesor.HttpContext.SignOutAsync();
            }
        }

        public async Task DeleteCurrent()
        {
            AppUser currentUser = await GetCurrentlySignedIn();

            if(currentUser != null)
            {
                string userId = currentUser.Id;
                
                await userManager.DeleteAsync(currentUser);
                await database.Events.RemoveAllForUser(userId);
            }
        }

        public async Task<string> GetCurrentUserId()
        {
            AppUser user = await GetCurrentlySignedIn();

            return user.Id;
        }
        public async Task<AppUser> GetCurrentlySignedIn()
        {
            if (!IsUserSignedIn())
            {
                throw new UserNotLoggedException();
            }

            var claims = httpContextAccesor.HttpContext.User;

            AppUser currentUser = await userManager.GetUserAsync(claims);

            return currentUser;
        }

        private bool IsUserSignedIn()
        {
            var user = httpContextAccesor.HttpContext?.User;

            if(user == null)
            {
                return false;
            }
          
            return user.Identity.IsAuthenticated;
        }
    }
}

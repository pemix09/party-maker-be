using Core.Models;
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
            AppUser user = await  userManager.FindByEmailAsync(_email);

            if (user == null)
            {
                throw new UserNotFoundException(_email);
            }

            if(!await signInManager.CanSignInAsync(user))
            {
                throw new UserCannotBeSignInException(user.UserName);
            }

            await signInManager.PasswordSignInAsync(_email, _password, stayLoggedAfterClosingBrowser, lockAccountAfterSignInFailure);
        }

        public async Task Logout()
        {
            if(IsUserSignedIn())
            {
                await signInManager.SignOutAsync();
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

        public async Task<AppUser> GetCurrent()
        {
            return await GetCurrentlySignedIn();
        }

        private bool IsUserSignedIn()
        {
            HttpContext httpContext = httpContextAccesor.HttpContext;

            if(httpContext != null)
            {
                ClaimsPrincipal userClaims = httpContext.User;
                return signInManager.IsSignedIn(userClaims);
            }
            return false;
        }

        private async Task<AppUser> GetCurrentlySignedIn()
        {
            HttpContext httpContext = httpContextAccesor.HttpContext;

            if (httpContext != null)
            {
                ClaimsPrincipal userClaims = httpContext.User;
                AppUser currentUser = await userManager.GetUserAsync(userClaims);

                return currentUser;
            }

            return null;
        }
    }
}

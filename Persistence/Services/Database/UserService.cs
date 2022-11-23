﻿using Core.Models;
using Core.UtilityClasses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Persistence.DbContext;
using Persistence.Exceptions;
using Persistence.Services.Utils;
using System.Security.Claims;

namespace Persistence.Services.Database
{
    public class UserService : ServiceBase, IUserService
    {
        private UserManager<AppUser> userManager { get; init; }
        private SignInManager<AppUser> signInManager { get; init; }
        private IHttpContextAccessor httpContextAccesor { get; init; }
        private TokenService tokenService { get; init; }
        private MailService mailService { get; init; }
        private bool stayLoggedAfterClosingBrowser = true;
        private bool lockAccountAfterSignInFailure = false;
        public UserService(
            PartyMakerDbContext _context,
            UserManager<AppUser> _userManager,
            SignInManager<AppUser> _signInManager,
            IHttpContextAccessor _httpContextAccesor,
            TokenService _tokenService,
            MailService _emailService) : base(_context)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            httpContextAccesor = _httpContextAccesor;
            tokenService = _tokenService;
            mailService = _emailService;
        }

        public async Task Register(AppUser _newUser, string _password)
        {
            var result = await userManager.CreateAsync(_newUser);

            if (!result.Succeeded)
            {
                throw new UserCannotBeCreatedException(result.Errors);
            }

            await userManager.AddPasswordAsync(_newUser, _password);
            await userManager.AddToRoleAsync(_newUser, "User");
            mailService.SendAccountConfirmation(_newUser.Email);
        }

        public async Task<LoginResponse> Login(string _email, string _password)
        {
            AppUser user = await userManager.FindByEmailAsync(_email);

            if (user == null)
            {
                throw new UserEmailNotFoundException(_email);
            }

            var refreshToken = tokenService.CreateRefreshToken();
            var accessToken = await tokenService.CreateAccessToken(user);

            user.SetRefreshToken(refreshToken);
            user.SetAccessToken(accessToken);

            await userManager.UpdateAsync(user);

            return new LoginResponse(accessToken, refreshToken.ToString());
        }

        public async Task Logout()
        {
            AppUser user = await GetCurrentlySignedIn();
            user.RefreshToken = String.Empty;
            await userManager.UpdateAsync(user);
        }

        public async Task DeleteCurrent()
        {
            AppUser user = await GetCurrentlySignedIn();
   
            string userId = user.Id;

            await userManager.DeleteAsync(user);
            await database.Events.RemoveAllForUser(userId);
        }
        public async Task<AppUser> GetCurrentlySignedIn()
        {
            try
            {
                var name = httpContextAccesor.HttpContext.User.Identity.Name;
                AppUser currentUser = await userManager.FindByNameAsync(name);
                return currentUser;
            }
            catch
            {
                throw new UserNotAuthenticatedException();
            }
        }

        public async Task<AppUser> GetUserById(string _Id)
        {
            AppUser user = await userManager.FindByIdAsync(_Id);

            if (user == null)
            {
                throw new UserIdNotFoundException(_Id);
            }
            else
            {
                return user;
            }
        }
    }
}

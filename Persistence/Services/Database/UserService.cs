﻿using Core.Dto;
using Core.Models;
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
        private UserManager<AppUser> UserManager { get; init; }
        private SignInManager<AppUser> SignInManager { get; init; }
        private IHttpContextAccessor HttpContextAccesor { get; init; }
        private TokenService TokenService { get; init; }
        private MailService MailService { get; init; }
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
            UserManager = _userManager;
            SignInManager = _signInManager;
            HttpContextAccesor = _httpContextAccesor;
            TokenService = _tokenService;
            MailService = _emailService;
        }

        public async Task Register(AppUser _newUser, string _password)
        {
            var result = await UserManager.CreateAsync(_newUser);

            if (!result.Succeeded)
            {
                throw new UserCannotBeCreatedException(result.Errors);
            }

            await UserManager.AddPasswordAsync(_newUser, _password);
            await UserManager.AddToRoleAsync(_newUser, "User");
            //mailService.SendAccountConfirmation(_newUser.Email);
        }

        public async Task ChangePassword(string _userId, string _oldPassword, string _newPassword)
        {
            var user = await UserManager.FindByIdAsync(_userId);
            if(user != null)
            {
                await UserManager.ChangePasswordAsync(user, _oldPassword, _newPassword);
            }
        }

        public async Task UpdateUser(AppUserDto user)
        {
            var userToUpdate = await UserManager.FindByIdAsync(user.Id);

            if(userToUpdate == null)
            {
                return;
            }
            if(userToUpdate.UserName != user.UserName)
            {
                var isUserNameTaken = await UserManager.FindByNameAsync(user.UserName);
                if(isUserNameTaken != null)
                {
                    throw new System.ComponentModel.DataAnnotations.ValidationException();
                }
                userToUpdate.UserName = user.UserName;
            }
            if(userToUpdate.Photo != user.Photo)
            {
                userToUpdate.Photo = user.Photo;
            }

            await UserManager.UpdateAsync(userToUpdate);
        }

        public async Task FollowEvent(int eventId)
        {
            var user = await GetCurrentlySignedIn();

            //initialize followed
            if(user != null && user.Followed == null)
            {
                user.Followed = new List<int>();
            }
            if (user != null && user.Followed.Contains(eventId) == false)
            {
                user.Followed.Add(eventId);
                await UserManager.UpdateAsync(user);
            }
        }

        public async Task UnFollowEvent(int eventId)
        {
            var user = await GetCurrentlySignedIn();

            //initialize followed
            if (user != null && user.Followed == null)
            {
                user.Followed = new List<int>();
            }
            if (user != null && user.Followed.Contains(eventId) == true) 
            {
                user.Followed.Remove(eventId);
                await UserManager.UpdateAsync(user);
            }
        }
        public async Task<LoginResponse> Login(string _email, string _password)
        {
            AppUser user = await UserManager.FindByEmailAsync(_email);

            if (user == null)
            {
                throw new UserEmailNotFoundException(_email);
            }

            if(user.RefreshToken == null)
            {
                var refreshToken = TokenService.CreateRefreshToken();
                user.SetRefreshToken(refreshToken);
            }
            else if(user.RefreshTokenExpires <= DateTime.Now)
            {
                user.RefreshTokenExpires = TokenService.GetNewRefreshTokenExpirationDate();
            }

            var accessToken = await TokenService.CreateAccessToken(user);
            user.SetAccessToken(accessToken);

            await UserManager.UpdateAsync(user);

            var refreshTokenFromUser = new RefreshToken()
            {
                Token = user.RefreshToken,
                Expires = user.RefreshTokenExpires,
                Created = user.RefreshTokenCreated
            };


            return new LoginResponse(accessToken, refreshTokenFromUser);
        }

        public async Task Logout()
        {
            AppUser user = await GetCurrentlySignedIn();
            user.RefreshToken = String.Empty;
            await UserManager.UpdateAsync(user);
        }

        public async Task DeleteCurrent()
        {
            AppUser user = await GetCurrentlySignedIn();
   
            string userId = user.Id;

            await UserManager.DeleteAsync(user);
            await database.Events.RemoveAllForUser(userId);
        }
        public async Task<AppUser> GetCurrentlySignedIn()
        {
            try
            {
                var name = HttpContextAccesor.HttpContext.User.Identity.Name;
                AppUser currentUser = await UserManager.FindByNameAsync(name);
                return currentUser;
            }
            catch
            {
                throw new UserNotAuthenticatedException();
            }
        }

        public async Task<AppUser> GetUserById(string _Id)
        {
            AppUser user = await UserManager.FindByIdAsync(_Id);

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

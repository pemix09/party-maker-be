using System.Security.Claims;
using Application.User.Commands;
using Application.User.Queries;
using Core.Dto;
using Core.Models;
using Infrastructure.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence.Exceptions;
using Persistence.Services.Database;
using Persistence.Services.Utils;

namespace API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : BaseCRUDController
    {
        private TokenService tokenService;
        private UserService userService;

        public UserController(IMediator _mediator, UserManager<AppUser> _userManager, TokenService _tokenService, UserService _userService) : base(_mediator)
        {
            userService = _userService;
            tokenService = _tokenService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody]RegisterUserCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Login([FromBody]LoginUserCommand command)
        {
            string token = await mediator.Send(command);
            return Ok(token);
        }

        [HttpGet]
        public async Task<IActionResult> Logout([FromQuery]LogoutUserCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet, Authorize(Roles = "User", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Delete([FromQuery]DeleteUserCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        //Authorize tag is for chechking if user is signed In
        [HttpGet, Authorize(Roles = "User", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<AppUserDto>> GetLogged([FromQuery]GetLoggedUserQuery query)
        {
            AppUserDto user = await mediator.Send(query);
            return Ok(user);
        }

        //TODO - to refactor to look better
        [HttpPost, Authorize(Roles = "User", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["RefreshToken"];
            var user = await userService.GetCurrentlySignedIn();

            if (user.RefreshToken.Equals(string.Empty))
            {
                throw new UserNotAuthenticatedException();
            }
            else if (!user.RefreshToken.Equals(refreshToken))
            {
                throw new InvalidRefreshTokenException();
            }
            else if (user.RefreshTokenExpires < DateTime.Now)
            {
                throw new TokenExpiredException();
            }

            string accessToken = await tokenService.CreateAccessToken(user);
            return Ok(accessToken);
        }
    }
}

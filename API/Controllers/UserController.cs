using System.Security.Claims;
using Application.User.Commands;
using Application.User.Queries;
using Core.Dto;
using Core.Models;
using Core.UtilityClasses;
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


        public UserController(IMediator _mediator) : base(_mediator)
        {
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
        public async Task<ActionResult<LoginResponse>> Login([FromBody]LoginUserCommand command)
        {
            var token = await mediator.Send(command);
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

        [HttpGet, Authorize(Roles = "User", AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult<AppUserDto>> GetLogged([FromQuery]GetLoggedUserQuery query)
        {
            AppUserDto user = await mediator.Send(query);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<AccessToken>> RefreshToken(RefreshTokenCommand command)
        {
            var token = await mediator.Send(command);
            return Ok(token);
        }
    }
}

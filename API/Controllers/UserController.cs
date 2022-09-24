using Application.User.Commands;
using Application.User.Queries;
using Core.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : BaseCRUDController
    {
        public UserController(IMediator _mediator) : base(_mediator) { }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterUserCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpPost]
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
    }
}

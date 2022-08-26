using Application.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : BaseCRUDController
    {
        public UserController(IMediator _mediator) : base(_mediator) { }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }
    }
}

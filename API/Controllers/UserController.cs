using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseCRUDController
    {
        public UserController(IMediator _mediator) : base(_mediator) { }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            mediator.Send(command);
        }
    }
}

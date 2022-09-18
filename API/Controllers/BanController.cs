using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    using Application.Ban.Commands;
    using Application.Ban.Queries;
    using Core.Models;
    using Microsoft.AspNetCore.Authorization;

    [Route("[controller]/[action]")]
    [ApiController]
    public class BanController : BaseCRUDController
    {
        public BanController(IMediator _mediator) : base(_mediator) { }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([FromBody] CreateBanCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update([FromBody] UpdateBanCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete([FromQuery] DeleteBanCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        //User should be able to see reason for the ban
        [HttpGet, Authorize(Roles = "User")]
        public async Task<ActionResult<Ban>> GetById([FromQuery] GetBanByIdQuery query)
        {
            Ban ban = await mediator.Send(query);
            return Ok(ban);
        }

        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Ban>>> GetAll([FromQuery] GetAllBansQuery query)
        {
            IEnumerable<Ban> bans = await mediator.Send(query);
            return Ok(bans);
        }
    }
}

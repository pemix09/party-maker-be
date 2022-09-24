using Application.EntrancePass.Commands;
using Application.EntrancePass.Queries;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //below attribute is for jwt authorization
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class EntrancePassController : BaseCRUDController
    {
        public EntrancePassController(IMediator _mediator) : base(_mediator){}
        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([FromBody] CreateEntrancePassCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update([FromBody] UpdateEntrancePassCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpDelete, Authorize(Roles ="Admin")]
        public async Task<ActionResult> Delete([FromQuery] DeleteEntrancePassCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet, Authorize(Roles = "User")]
        public async Task<ActionResult<Event>> GetById([FromQuery] GetEntrancePassByIdQuery query)
        {
            EntrancePass pass = await mediator.Send(query);
            return Ok(pass);
        }

        [HttpGet, Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<EntrancePass>>> GetAll([FromQuery] GetAllEntrancePassesQuery query)
        {
            IEnumerable<EntrancePass> passes = await mediator.Send(query);
            return Ok(passes);
        }
    }
}

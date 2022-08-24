using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    using Core.Models;

    [Route("[controller]/[action]")]
    [ApiController]
    public class BanController : BaseCRUDController
    {
        public BanController(IMediator _mediator) : base(_mediator) { }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateEventCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateEventCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] DeleteEventCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<Ban>> GetById([FromQuery] GetEventByIdQuery query)
        {
            Ban ban = await mediator.Send(query);
            return Ok(ban);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ban>>> GetAll([FromQuery] GetAllEventsQuery query)
        {
            IEnumerable<Ban> bans = await mediator.Send(query);
            return Ok(bans);
        }
    }
}

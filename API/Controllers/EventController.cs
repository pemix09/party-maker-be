using Application.Message.Queries;
using Core.Models;

namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Application.Message.Commands;
    
    [Route("[controller]/[action]")]
    [ApiController]
    public class EventController : BaseCRUDController
    {
        public EventController(IMediator _mediator) : base(_mediator) { }

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
        public async Task<ActionResult<Event>> GetById([FromQuery] GetEventByIdQuery query)
        {
            Event _event = await mediator.Send(query);
            return Ok(_event);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetAll([FromQuery] GetAllEventsQuery query)
        {
            IEnumerable<Event> events = await mediator.Send(query);
            return Ok(events);
        }
    }
}
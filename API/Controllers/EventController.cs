using Application.Message.Queries;
using Core.Models;

namespace API.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Application.Message.Commands;
    using Microsoft.AspNetCore.Authorization;
    using Application.Event.Queries;
    using Application.Event.Commands;

    //below attribute is for jwt authorization
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class EventController : BaseCRUDController
    {
        public EventController(IMediator _mediator) : base(_mediator) { }

        [HttpPost, Authorize(Roles ="User")]
        public async Task<ActionResult> Create([FromBody] CreateEventCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpPut, Authorize(Roles = "User")]
        public async Task<ActionResult> Update([FromBody] UpdateEventCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpDelete, Authorize(Roles = "User")]
        public async Task<ActionResult> Delete([FromQuery] DeleteEventCommand command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpGet, Authorize(Roles = "User")]
        public async Task<ActionResult<Event>> GetById([FromQuery] GetEventByIdQuery query)
        {
            Event _event = await mediator.Send(query);
            return Ok(_event);
        }

        [HttpGet, Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<Event>>> GetForAreaByQuery([FromQuery] GetAllEventsQuery query)
        {
            IEnumerable<Event> events = await mediator.Send(query);
            return Ok(events);
        }

        [HttpGet, Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<Event>>> GetForArea([FromQuery] GetAllForArea query)
        {
            return Ok(await mediator.Send(query));
        }

        [HttpGet, Authorize(Roles = "User")]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllOfCurrentUser([FromQuery] GetAllOfCurrentUserQuery query)
        {
            return Ok(await mediator.Send(query));
        }

        [HttpPost, Authorize(Roles = "User")]
        public async Task<ActionResult> FollowEvent([FromBody] FollowEventCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpPost, Authorize(Roles = "User")]
        public async Task<ActionResult> UnfollowEvent([FromBody] UnfollowEventCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}